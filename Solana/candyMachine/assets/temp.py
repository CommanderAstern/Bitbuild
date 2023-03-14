import os
import shutil

# Define the name of the original folder
original_folder_name = "C:\\Users\\asimj\\Documents\\bitbuild\\Solana\\candyMachine\\assets\\original_folder"

# Define the name of the new folder
new_folder_name = "C:\\Users\\asimj\\Documents\\bitbuild\\Solana\\candyMachine\\assets\\new_folder"

# Define the number of copies to create
num_copies = 10

# Create the new folder
os.mkdir(new_folder_name)

temp = 25
# Loop through the number of copies to create
for i in range(num_copies):
    # Loop through the files in the original folder
    for j in range(25):
        # Define the old file paths
        old_png_path = f"{original_folder_name}/{j}.png"
        old_json_path = f"{original_folder_name}/{j}.json"

        # Define the new file paths
        new_png_path = f"{original_folder_name}/{temp}.png"
        new_json_path = f"{original_folder_name}/{temp}.json"
        temp += 1
        # Copy the files and rename them
        shutil.copy(old_png_path, new_png_path)
        shutil.copy(old_json_path, new_json_path)

# Rename the copied files to start from 0
for i, file_name in enumerate(sorted(os.listdir(new_folder_name))):
    # Define the old file path
    old_path = os.path.join(new_folder_name, file_name)

    # Define the new file path
    new_path = os.path.join(new_folder_name, f"{i}.{'.'.join(file_name.split('.')[1:])}")

    # Rename the file
    os.rename(old_path, new_path)
