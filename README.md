Airlocker
=========

Airlocker is a lightweight yet effective backup software. Right-click your folder, select "Send To -> Airlocker", and that's all! Next time you do it, only new & changed files will be copied.

Sorry that I didn't have the time to prepare a setup program. Here are the manual installation instructions:

    Extract Airlocker.exe, AirlockConfigurer.exe and AirlockConfig.dll to a folder (I assume c:\program files\airlock\)
    Run AirlockConfigurer.exe and select your target backup folder. This may be a folder on your harddisk, or the root of a removable storage device
    Vista users; create a shortcut in C:\Users\<username>\AppData\Roaming\Microsoft\Windows\SendTo (2000 / XP users might find the SENDTO folder elsewhere). The shortcut will look like this:
        E:\Development\Airlock\Airlocker\bin\Debug\Airlocker.exe -c &1 (to copy files)
        E:\Development\Airlock\Airlocker\bin\Debug\Airlocker.exe -m &1 (to move files)
    That's it! Now you can right-click a folder and select the new item to backup your files.
    
How does Airlocker backup my files?

    Let's assume your computer name is "HomePC1". When you right-clicked C:\Users\Kerem\Documents and back it up, here is the sequence that Airlocker will follow:
        A folder called "HomePC1" will be created on the target device
        A folder called "HomePC1\C" will be created on the target device
        A folder called "HomePC1\C\Users" will be created on the target device
        A folder called "HomePC1\C\Users\Kerem" will be created on the target device
        A folder called "HomePC1\C\Users\Kerem\Documents" will be created on the target device
        All of the new & changed files in C:\Users\Kerem\Documents will be copied to (let's say) "H:\HomePC1\C\Users\Kerem\Documents"

So, only new & changed files are copied. Why?

    If you have an archive of 10 GB's, you wouldn't want to wait for all of the files to be copied. Airlocker copies only new & changed files, saving lots of time. That way, you are ensured that your whole archive is safe on the remote device.

Can I use this program on multiple PC's?

    Absolutely! Backups of multiple PC's will be stored in different folders on the remote device. For instance...
        ...your documents in "HomePC1" will be stored in "H:\HomePC1\C\Users\Kerem\Documents"
        ...your documents in "MyLaptop" will be stored in "H:\MyLaptop\C\Users\Kerem\Documents"

How is this different from Windows' file copy?

    Making backups through the standard copy operation is very slow because you are practically copying all files to the remote folder. Airlocker copies new / changed files only.

How is this different from Vista's Briefcase?

    Actually it's not too different; except the following points:
        Briefcase doesn't copy files which require administrative authorization. Airlocker does (I think).
        Briefcase may give you a hard time when you try to access the files in a non-Vista system. Airlocker merely copies files and touches nothing on th file system; ensuring that files on the target device will be accessible from any OS.
        Managing backup operations of multiple PC's on a single device is easier and more standardized in Airlocker.
        Briefcase updates files in both directions. Although small, there is a risk of ruining your file at your PC because the file on your removable media has been modified somehow. Airlocker updates file in one direction (PC -> Media); and never touches the files on your PC.
