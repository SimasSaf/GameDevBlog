# Rollaball (#1)

Following the rollaball tutorial provided by the GMD1 teacher Jakob Rasmussen I created a Unity project. The game consists of a rolling ball, controlled using WASD, a playable area and "pick ups".

The win condition is 30 collectables. (its a bit much but its just a "You Win!" text).

![Game Scene](/Blog/resources/rollaball1.png)

![Win Screen](/Blog/resources/rollaball2.png)

The tutorial showed how to add a player controlled sphere with inertia and physics, a plane on which the sphere rolls around, walls so that the player would not roll off, objects that rotate and can be picked up and a HUD for the player to see progress with a text element which signifies the win condition being fulfilled.
After completing the tutorial I went on to add assets from the Unity store. These include food items which cycle through 4 different models constantly and textures. After wrote custom script logic for spawning new pick ups after they have been collected which also increase the size of the ball. I also altered existing code for the items being "eaten", some of them were disappearing for a second after completing the rotation cycle.