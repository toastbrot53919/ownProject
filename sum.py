import os
import openai

# Set up the OpenAI Codex model
openai.api_key = "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"

def summarize_code(code: str, summary_path: str, overwrite: bool) -> str:
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

def summarize_folder_structure(folder_structure: dict, base_path: str, summarized_structure: dict, overwrite: bool):
    for key, value in folder_structure.items():
        if key == 'files':
            file_summaries = []
            for file in value:
                with open(file, "r") as file_obj:
                    code = file_obj.read()
                    summary = summarize_code(code, f"{os.path.splitext(file)[0]}_summary.txt", overwrite)
                    file_summaries.append(summary)
                    save_summary(summary, f"{os.path.splitext(file)[0]}_summary.txt", overwrite)

            folder_summary = summarize_summaries(file_summaries, f"{base_path}_summary.txt", overwrite)
            summarized_structure[base_path] = folder_summary
            save_summary(folder_summary, f"{base_path}_summary.txt", overwrite)
        else:
            summarize_folder_structure(value, os.path.join(base_path, key), summarized_structure, overwrite)


def main(directory: str, overwrite: bool = False):
    folder_structure = build_folder_structure(directory)
    summarized_structure = {}
    summarize_folder_structure(folder_structure, directory, summarized_structure, overwrite)
    print("\nSUMMARIZED STRUCTURE\n")
    print(summarized_structure)

if __name__ == "__main__":
    import argparse
    parser = argparse.ArgumentParser(description='Generate summaries for .cs files.')
    parser.add_argument('directory', type=str, help='Directory to summarize.')
    parser.add_argument('--overwrite', action='store_true', help='Overwrite existing summary files.')
    args = parser.parse_args()
    main(args.directory, args.overwrite)

