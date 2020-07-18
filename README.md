# CookTime project
## Welcome to the readme file for this project!
"CookTime" is the name given to the second programming projects' assignment, first term of 2020 (I-2020) for Algorithms and Data Structures I, code  CE-1103 at Costa Rican Institute of Technology.

The project consists of a client-server implementation, using a REST API to connect the client and server applications.

### Technical aspects:
Server and REST are being coded in Java, using the 13th release of JDK. (13.0.x)
Client-related code will be implemented in C#, due to the fact that it is based in the Xamarin framework.

### Setup Instructions
In order to load the data base, you need to add the following VM argument:

>`-Dproject.folder="path/to/project/folder"`
make sure to change `"path/to/project/folder"` to your project directory

Also, when cloning the repository to a local directory, you might encounter build errors on the server folder when running in any Java IDE. This is caused by missing java libraries that IDEs such as IntelliJ IDEA can automatically install. Simply check the imports reported as missing, download the needed `.jar` files, and rebuild the project.

### External Documentation
This project is documented in the wiki page for this same repository, which you can access [here](https://github.com/JFPenguin/CookTime/wiki).
#### Please note: the external documentation content is in spanish.
