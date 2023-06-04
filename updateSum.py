import os
import openai
from collections import defaultdict

# Set up the OpenAI Codex model
openai.api_key = "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"



# Function to generate summaries using Codex
def summarize_code(code: str,summary_path: str, overwrite: bool) -> str:
	if not overwrite and os.path.exists(summary_path):
		print(f"{summary_path} already exists. Skipping...")
		with open(summary_path, "r") as summary_file:
			summary = summary_file.read()
	else:
		completion = openai.ChatCompletion.create(
			model="gpt-3.5-turbo",
			messages=[
			{"role": "user", "content": f"You are a Unity Developer .Your task is to Summeries it's intern logic and implementation.Start with the name of the file. do not ignore important details.Try to reduce the Tokens that you use.write text don't write code.be explict: {code}"}
			],
		)
		summary = completion.choices[0].message.content
		print("\n Summery \n")
		print(summary)

	return summary

def summarize_summaries(summaries: list, summary_path: str, overwrite: bool) -> str:
	if not overwrite and os.path.exists(summary_path):
		print(f"{summary_path} already exists. Skipping...")
		with open(summary_path, "r") as summary_file:
			summary = summary_file.read()
	else:
		concatenated_summaries = " ".join(summaries)
		response = openai.ChatCompletion.create(
			model="gpt-3.5-turbo",
			messages=[
			{"role": "user", "content": f" You are a Unity Developer . Your task is to Summeries the implementation of this summeries.Start with the names of the classes. do not ignore important details.Try to reduce the Tokens that you use.write text.be explict: {concatenated_summaries}"}
			],
		)
		summary = response.choices[0].message.content
		print("\nBIG SUMMERY\n")
		print(summary)
			
	return summary

def build_folder_structure(root: str):
    folder_structure = {}

    for directory, subdirs, files in os.walk(root):
        cs_files = [os.path.join(directory, file) for file in files if file.endswith(".cs")]

        if cs_files:
            current_level = folder_structure
            directory_parts = directory.replace(root, "").split(os.sep)

            for part in directory_parts:
                if part != '':  # Skip the empty strings
                    if part not in current_level:
                        current_level[part] = {}
                    current_level = current_level[part]

            current_level['files'] = cs_files

    return folder_structure

def save_summary(summary: str, summary_path: str, overwrite: bool):
    if not overwrite and os.path.exists(summary_path):
        print(f"{summary_path} already exists. Skipping...")
        return

    with open(summary_path, "w") as summary_file:
        summary_file.write(summary)


def summarize_folder_structure(folder_structure: dict, summarized_structure: dict, overwrite: bool):
    for folder, content in folder_structure.items():
        if isinstance(content, dict):  # if content is a subfolder
            summarize_folder_structure(content, summarized_structure, overwrite)  # Recurse into subfolders
        else:  # if content is a list of files
            file_summaries = []
            for file in content:
                with open(file, "r") as file_obj:
                    code = file_obj.read()
                    summary = summarize_code(code, f"{os.path.splitext(file)[0]}_summary.txt", overwrite)
                    file_summaries.append(summary)
                    save_summary(summary, f"{os.path.splitext(file)[0]}_summary.txt", overwrite)
            
            # At this point, file_summaries contains summaries of all files in current folder
            folder_summary = summarize_summaries(file_summaries, f"{folder}_summary.txt", overwrite)
            summarized_structure[folder] = folder_summary
            save_summary(folder_summary, f"{folder}_summary.txt", overwrite)
	
def save_summarized_structure(summarized_structure: dict, output_file: str):
    with open(output_file, "w") as file:
        for folder, summary in summarized_structure.items():
            file.write(f"{folder}:\n{summary}\n\n")

def main():
    directory = r"C:\Users\Toastbrot\Downloads\STRATEGY 01.04.2022\My project\Assets\Scripts"
    folder_structure = build_folder_structure(directory)
    sorted_folders = sorted(folder_structure.keys(), key=lambda x: x.count(os.sep), reverse=True)

    summarized_structure = {}

    overwrite = input("Do you want to overwrite existing summary files? (y/n): ").lower() == "y"
 
    save_summarized_structure(summarized_structure, "summarized_structure.txt")

    for folder, summary in summarized_structure.items():
        print(f"{folder}: {summary}")

if __name__ == "__main__":
    main()