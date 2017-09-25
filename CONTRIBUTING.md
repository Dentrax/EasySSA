[Contributing](#contributing)  
[Resolving Merge Conflicts](#resolving-merge-conflicts)  
[General resources](#general-resources)  
[Style Guidelines](#style-guidelines)  
[Best Practices for Contributing](#best-practices-for-contributing)    

# Note

Please also read the [Contributor's Portal](../../wiki/Contributors'-Portal#important-links), mainly the [Contributing Guidelines](../../wiki/Contributing-Guidelines).

# Contributing

If you would like to contribute to this project by modifying/adding to the program code or various creative assets, read the [Best Practices for Contributing] below and feel free to follow the standard Github workflow:

1. Fork the project.
2. Clone your fork to your computer.
 * From the command line: `git clone https://github.com/<USERNAME>/EasySSA.git`
3. Change into your new project folder.
 * From the command line: `cd EasySQLITE`
4. [optional]  Add the upstream repository to your list of remotes.
 * From the command line: `git remote add upstream https://github.com/EasySSA/EasySSA.git`
5. Create a branch for your new feature.
 * From the command line: `git checkout -b my-feature-branch-name`
6. Make your changes.
 * Avoid making changes to more files than necessary for your feature (i.e. refrain from combining your "real" pull request with incidental bug fixes). This will simplify the merging process and make your changes clearer.
 * Very much avoid making changes to the Unity-specific files, like the scene and the project settings unless absolutely necessary. Changes here are very likely to cause difficult to merge conflicts. Work in code as much as possible. (We will be trying to change the UI to be more code-driven in the future.) Making changes to prefabs should generally be safe -- but create a copy of the main scene and work there instead (then delete your copy of the scene before committing).
7. Commit your changes. From the command line:
 * `git add my-changed-file.cs`
 * `git add my-other-changed-file.cs`
 * `git commit -m "A descriptive commit message"`
8. While you were working some other pull request might have gone in the breaks your stuff or vice versa. This can be a *merge conflict* but also conflicting game logic or code. Before you test, merge with master.
 * `git fetch upstream`
 * `git merge upstream/master`
9. Test. Start the game and do something related to your feature/fix.
10. Push the branch, uploading it to Github.
  * `git push origin my-feature-branch-name`
11. Make a "Pull Request" from your branch here on Github.
  * Include screenshots demonstrating your change if applicable.
  
  # Resolving Merge Conflicts

Depending on the order that Pull Requests get processed, your PR may result in a conflict and become un-mergable.  To correct this, do the following from the command line:

Switch to your branch: `git checkout my-feature-branch-name`
Pull in the latest upstream changes: `git pull upstream master`
Find out what files have a conflict: `git status`

Edit the conflicting file(s) and look for a block that looks like this:
```
<<<<<<< HEAD
my awesome change
=======
some other person's less awesome change
>>>>>>> some-branch
```

Replace all five (or more) lines with the correct version (yours, theirs, or
a combination of the two).  ONLY the correct content should remain (none of
that `<<<<< HEAD` stuff.)

Then re-commit and re-push the file.

```
git add the-changed-file.cs
git commit -m "Resolved conflict between this and PR #123"
git push origin my-feature-branch-name
```

The pull request should automatically update to reflect your changes.

# General resources
* [GitHub Forking Overview](https://gist.github.com/Chaser324/ce0505fbed06b947d962)
* [GitHub Documentation for Desktop Client](https://help.github.com/desktop/guides/contributing/)
* [GitHub Desktop Client](https://desktop.github.com/)
* [GitHub for Windows](https://git-for-windows.github.io/)
* [Android Studio](https://developer.android.com/studio/index.html)

## Style Guidelines

As a TL;DR on our coding practices, adhere to the following example:

```c#
// All files begin with the following license header (remove this line):
#region License
// ====================================================
// EasySQLITE Copyright(C) 2017 EasySSA
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

// Use camelCasing unless stated otherwise.
// Descriptive names for variables/methods should be used.
// Fields, properties and methods should always specify their scope, aka private/protected/internal/public.

// Interfaces start with an I and should use PascalCasing.
public interface IInterfaceable {

}

// Class names should use PascalCasing.
// Braces are not be new line.

public class Class {
    // Private fields should be m_camelCased.
    // Use properties for any field that needs access levels other than private
    private string m_someField;

    // Events should use PascalCasing as well.
    // ✓ DO name events with a verb or a verb phrase.
    // Examples include Clicked, Painting, DroppedDown, and so on.
    // ✓ DO give events names with a concept of before and after, using the present and past tenses.
    // For example, a close event that is raised before a window is closed would be called Closing,
    // and one that is raised after the window is closed would be called Closed.
    public event EventHandler<EventArgs> SomeEvent;
    
    public static readonly var SOME_READONLY_VAR;

    // Properties should use PascalCasing.
    public int MemberProperty { get; private set; }

    // Methods should use PascalCasing.
    // Method parameters should be camelCased.
    public void SomeMethod(int functionParameter) {
        // Local variables should also be camelCased.
        int myLocalVariable = 0;
    }
}
```
## Best Practices for Contributing
[Best Practices for Contributing]: #best-practices-for-contributing
* UPDATE SOON...
