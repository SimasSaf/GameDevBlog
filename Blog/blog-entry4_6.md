# Fourth Blog Entry, Final Game Show Off (#6)

The player controls a cannon which spins around the earth. The cannon autofires constantly. Enemies start spawning fromt he sides and approaching the player. When an enemy is killed, he gives experience, when the player levels up he gets to choose 1 from 4 upgrades.
- damage upgrade increases damage by 5
- fire rate upgrade increases the firerate by 0.1s
- split level upgrade makes it so that when the player kills an enemy with a bullet, another bullet is spawned from the enemy into a random direction. Increasing this level increases the amount of bullets. These bullets keep the new upgrades such as fire level, damage, split level.
- fire level upgrade sets enemies on fire, they then take damage until they die. After level 3, enemies which are on fire, explode dealing damage around them. After fire level in 10+ the explosion also sets the enemies on fire, continueing the chain. Each level also increases the damage of the passive fire damage by 2 and the explosion damage by 5.

![Gameplay](/Blog/resources/entry4-gameplay.png)

The red enemies are enemies on fire, the explosion can also be seen. The yellow dots are bullets. The game also has damage numbers displaying how much damage the player is doing to the enemies.

![Upgrades](/Blog/resources/entry4-upgrades.png)

The levels are infinite. However the enmies scale by the players level, they gain some move speed but they gain 10 health per level.

The game also has sound. The sounds included are menu music, ingame music and shooting sounds.

## Conclusion

The game works good, I am happy with the outcome, I have many more ideas, ended up diverging from the original idea and features to fit the time constraint.

## WebGL Build

The WebGL build has a bug where most text disappears. Not sure why this happens but a browser that WORKS is Mozilla. Browser that DONT work: Opera, Chrome.

https://simassaf.github.io/LifeDefendersWebBuild/
