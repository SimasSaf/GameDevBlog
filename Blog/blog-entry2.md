# Second Blog Entry (The Losening of the Mind)

So i had a little breakdown when looking at my code and ended up rewriting the logic until im at least somewhat satisfied.

Sorry if the project is a bit small. Had to do a lot of thinking and refactoring and completely rewriting classes, implementing interfaces thinking where to use what design patterns such as observers. The project might not entirely reflect the work that went into this. Just wanted better code for myself as hopefully later in life i develop more games and don`t feel spooked out to do so.

Side Note: I am using screenshots for code, for better visibility with the added colors of the editor.

## UI

### The look of the UI (not set in stone)

The UI needs clean up to be beautiful, but logically does the job.

In the main menu i don`t pause the time, that was a stylistic choice to see the cannon firing. But opening the PauseMenu will pause the game.

![UI-MainMenu](/Blog/resources/entry2-MainMenu.png)
![UI-SettingsMenu](/Blog/resources/entry2-UI-Settings.png)
![UI-PauseMenu](/Blog/resources/entry2-UI-PauseMenu.png)
![UI-IngameUI](/Blog/resources/entry2-UI-IngameUI.png)

The Ingame UI now has an exp bar, a level counter, health. On level up we can see a choice pop up of 4 options to increase.

Not seen here but there are also damage numbers added when the bullet triggers an enemy.

### UI Code

The whole "UI" now has a MenuUIController which is responsible for methods such as "StartGame()" which "Starts the game" :D. Starts the music, moves the camera where its needed and is responsible for "Activating" UI game objects. Handles logic which switches UI screens/menus (example: NavigateToMainMenu()).

![MenuUIController](/Blog/resources/entry2-MenuUIController.png)

The menu also uses a "weird" interface which is not quite necessary but i felt like making it. The IUIActivator which is based on the UIActivator:

![UIActivator](/Blog/resources/entry2-UIActivator.png)

All it does is setActive the respective menus.

The menus are responsible for managing the button presses. They depend on the MenuUIController interface. I did this so that for example if the logic of StartGame() would change. It makes sense to me that we could switch out the method or the whole MenuUIController rather than the actual button click. And the button Single-Player will still start the game, no worries of the logic.

![MainMenu](/Blog/resources/entry2-MainMenu.png)

The navigation of the menu can be done with a mouse or with the EventSystem. Works good, tested on the machine also works.

## Sound

Added sound music for MainMenu and the game seperately. Also i use an interface to call these methods from other scripts.

![MusicCode](/Blog/resources/entry2-musicCode.png)
![ShootingSound](/Blog/resources/entry2-shootingSound.png)

The shooting basically plays a sound and only stops it when it ends. If it would be .Play() another instance of a shot being fired would end the previous sound clip. Because the fire rate is destened to change i need each shot to make a sound seperately.

## Health System

The health system uses Observers.

![HealthObserver](/Blog/resources/entry2-helthObserver.png)

When earth takes damage (which happens when an enemy collides with earth) all the observers are notified. Then they do whatever needs to be done. For example: updates the UI with the current and max health or displays game over screen in the player/earth dies.


## Spawning enemies

Enemies spawn outside the screen with a random gap inbetween so they would not spawn in a unit circle. They move towards the earth in a straight line. The enemies are Kinematic, meaning they need a enemy movement script to move. The earth is a trigger for them and bullets are collision based. This is done so that earth and enemies could interact with eachother. This was the only solution that was working with static and kinematic interactions.

![EnemySpawner](/Blog/resources/entry2-spawnEnamies.png)

## Upgrades

There is an upgrade system that i hard coded in to a specific value, could not be bothered to make it dynamic (preferably this would be for the future). The 4 upgrades are damage, fire rate, split bullets and set things on fire (fire is not implemented yet).

Split bullets is the interesting one where it makes it so when a bullet kills an enemy, more bullets are spawned from the corpse in a random direction. These bullets are initialized with all the same properties as other bullets. So technically those bullets will set things on fire and do the same damage and split even more to a chain reaction.

![BulletSpawner](/Blog/resources/entry2-bulletSpawner.png)

Basically each bullet is taken from a pool, has a force applied to it to move it and is initialized with some behaviors and stats. It also has a cutoff point when to take thebullet out of the "simulation" (to deactivate it from the pool). Each bullet interacts with enemies on collision with the behavior script. Based on that it will set the enemies on fire and based on the enemy state, they will explode.

## Side note
I might have missed some changes as this was in the making for a while. Sorry in advance, the bachelor has us stressing.

Best regards,
Simas Safronvoas