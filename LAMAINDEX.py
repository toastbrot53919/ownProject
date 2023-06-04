import os
from llama_index import ServiceContext
from llama_index import download_loader, GPTVectorStoreIndex, Document
from llama_index import ServiceContext
from pathlib import Path
from llama_index.langchain_helpers.agents import LlamaToolkit, create_llama_chat_agent, IndexToolConfig
from llama_index.indices.query.query_transform.base import DecomposeQueryTransform
from llama_index.query_engine.transform_query_engine import TransformQueryEngine
from langchain.agents import Tool
from langchain.chains.conversation.memory import ConversationBufferMemory
from langchain.chat_models import ChatOpenAI
from langchain.agents import initialize_agent
from llama_index import LLMPredictor
from langchain import OpenAI

file_directory = "C:/Users/Toastbrot/Downloads/STRATEGY 01.04.2022/My project/Assets/Scripts/"

# Read all C# files in the directory
# Read all C# files in the directory and its subfolders
documents = []
file_names = []
for root, _, files in os.walk(file_directory):  # <- Modification: Traverse directories recursively
    for file_name in files:
        if file_name.endswith(".cs"):
            print(f"Processing: {file_name}")  # <- Debug print
            file_path = os.path.join(root, file_name)  # <- Modification: Include subfolder path
            with open(file_path, "r") as f:
                data = f.read()
            doc = Document(text=data)
            documents.append(doc)
            file_names.append(file_name)


print(f"Total documents: {len(documents)}")  # <- Modification: Debug print

# Initialize vector indices for each document
service_context = ServiceContext.from_defaults(chunk_size_limit=512)
indices = [GPTVectorStoreIndex.from_documents([doc], service_context=service_context) for doc in documents]

# Create index configurations for each class
index_configs = []
for index, file_name in zip(indices, file_names):
    tool_config = IndexToolConfig(
        query_engine=index.as_query_engine(similarity_top_k=3),
        name=f"Vector Index - {file_name}",
        description=f"Useful for when you want to answer queries about the {file_name} class",
        tool_kwargs={"return_direct": True, "return_sources": True},
    )
    index_configs.append(tool_config)

toolkit = LlamaToolkit(index_configs=index_configs)
memory = ConversationBufferMemory(memory_key="chat_history")

llm = LLMPredictor(llm=OpenAI(temperature=0, max_tokens=512))

agent_chain = create_llama_chat_agent(toolkit, llm.llm, memory=memory)

while True:
    text_input = input("User: ")
    response = agent_chain.run(input=text_input)
    print(f'Agent: {response}')
    text_input = input("are you finished ?yes/no")
    response = agent_chain.run(input=text_input)
    while response=="no":
        text_input = "Continue on your last line"
        response = agent_chain.run(input=text_input)
        print(f'Agent: {response}')
        text_input = input("are you finished ?yes/no")
        response = agent_chain.run(input=text_input)
    print(f'Agent: Finsihed!')
