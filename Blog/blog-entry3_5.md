# Third Blog Entry (#5)

Might miss quite a few things that were added this addition, lost track a bit due to the exam period.

## Logic

- Added an upgrade fireLevel which sets enemies on fire, if they die while on fire they explode.

![FireLogic](/Blog/resources/entry3-fireLogic.png)

Summons an explosion that deals damage to enemies. This is also only done after fireLevel is 3 >. When the enemy is set on fire, the color changes and applies damage.

- Added enemy scaling per player level. Now the enemies get increased HP and speed when the user becomes stronger. This is done by just adding a few values to be multiplied by the amount gotten from the levelingSystem.
- Added button to navigate back to main menu after game over.
- Added logic to reset stats for a new game. When the notification sends the reset, the observers fire onReset where manually the values are reset.

## Assets

- Added a new font used in the game. The font i got from daFont.com
- Changed the background image to have some color
- Hand made SVG assets with Adobe Illustrator: earth, cannon, enemy1, enemy2, enemy3.
- Added logic to color the assets red when on fire
- Made custom flying, death animations for the enemy assets

The enemy prefabs have different parts seperated for animation purposes.

![EnemyAnimation](/Blog/resources/entry3-enemyAnimation.png)

The animation is done manually, adjusting the position, rotation, scale, transperancy of the elements.

Example how i made it in illustrator:

![IllustratorExample](/Blog/resources/entry3-illustrator.png)

Then i cut it in multiple parts in unity and use them to animate my prefab through the animator controller. The death animation is done the same way. Fir triggering the states I use the trigger in the animator then set it when needed and reset it when it has to be returned to pool.


## Balancing

Not sure if i am still perfectly satisfied with the balancing, i had a few friends test the game and made some changes. Before the start was quite diffucult while later the game becomes  abreeze. Changed some damage numbers, enemy scaling to fit better. Now the game also has a increasing enemy count as the levels progress. After level 5, 10, 20 more enemies are spawned.

## Conclusion

For this blog post i am not showing much as it is a lot of cleaning up and balancing. This can be seen in the final post which shows of the final product.