The Draw Call Minimizer is the next step in optimizing your environments.



What Draw Call Minimizer does is create a texture atlas and as few 
meshes as possible (Unity has a 65000 vertice limit on objects)
no matter how many materials are childed under the game object.



You can use this tool is whichever project you wish, all I ask for is a special thanks 
to Purdyjo in the credits or some sort of recognition.

New in Version 1.3:

Support for multiple shaders!

Now Draw Call Minimizer will create objects based on their shader type, and creates texture atlases for each shader.

It can now also support custom shaders that you have programmed yourself, all you need to do is enter what you named
the shader properties into the script.