# GUIpp
A WYSIWYG editor for [UI++](http://uiplusplus.configmgrftw.com) (Currently in Alpha)

Well, we're finally here... the alpha release. I guarantee there are still quite a few bugs that I have not teased out yet,
so if you find something of issue track it in issues. Please include the following with your issue:
 - What specifically you're having trouble with
 - What you were doing when you encountered the issue
 - Steps to reproduce (if you can replicate)
 - If you're comfortable, send me a copy of your XML to guipp@z-nerd.com
 
## Notes on Use
Right now (G)UI++ supports the beta version of UI++. Please review the [changelog](https://beta.uiplusplus.configmgrftw.com/download/) for applicable changes.
Here are some of the known... features... of (G)UI++
 - When creating an AppTree, you'll notice you don't have the option to add a SoftwareSets element (and if you import, it's missing). This is because
   the SoftwareSets element has no attributes (e.g. cannot add a condition), and therefore only serves as a container for the AppTree Set elements.
   Don't worry, I'm handling the addition of the SoftwareSets element in the code, you don't have to do it.
 - All inputs are updated to their new type:
   - TextInput, ChoiceInput, and CheckboxInput have all been renamed to InputCheckbox, InputChoice, and InputText. This is not compatible with UI++ < 3.0
 - Comments are not maintained
   - This is a slightly complicated issue, since I'm converting the XML into C# classes, and theoretically comments could be anywhere in the XML.
   - My belief is that comments won't be necessary with a WYSIWYG editor, but you can weigh in on that discussion here: https://github.com/theznerd/GUIpp/issues/4
 - A handful of features (as documented in the UI++ 3.0 beta changelog) in (G)UI++ are only supported by UI++ 3.0. If you're not using UI++
   3.0, then you might have a bad time.
 - 4k monitor support is still touchy. The Info and ErrorInfo actions especially. Feel free to track this issue here: https://github.com/theznerd/GUIpp/issues/5
 
 ## Download
 You can get the latest version of the application here: https://github.com/theznerd/GUIpp/releases
 
 ## Changelog
 0.1.1.0-alpha
  - Fixes importing UI++ XML with comments
  - Fixes color import for UI++ XML color attributes without leading #
  - Changes XML output to use single-quote as default
  
 0.1.0.0-alpha
  - Initial Release
