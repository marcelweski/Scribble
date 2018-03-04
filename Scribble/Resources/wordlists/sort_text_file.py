
path = input("Path> ")

lines = []
with open(path, "r") as f:
    lines = f.readlines()
f.close()

with open(path, "w") as f:
    f.writelines(sorted(lines, key=lambda x: x.lower()))
f.close()
