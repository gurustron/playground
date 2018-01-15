with open("passwords.txt") as f, open("new.txt", "w+") as n:
    lines = f.readlines()
    lines.reverse()
    n.writelines(lines)
