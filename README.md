# Spoopy Treat

*Spoopy Treat* is a Unity game made for a school's recruiting process. Enjoying the concept I've found for it so much, I've decided to turn it into an actual game and will ship it as a Mofumofu product. It will be a 3D platformer, and this repository holds the demo version.  

**DISCLAIMERS**
For a given version, only the most recent sub-version builds are available. If you are interested in testing earlier sub-versions, a link to each sub-version's last commit is available in the notes down below. You are allowed to clone and build from Unity.  
You are however not allowed to claim this project as your own, even if the version you use is a pre-release one or a demo available for free.  
And when it comes to using the assets, you are of course allowed to dispose of them as you will as long as you have the authorization of the respective owners.  

---

## DOWNLOAD LINKS

**VERSION 0.0.0 --> 0.0.0-a (30/10/22) / 0.0.0-b (15/11/22) / 0.0.0-c (ongoing)**  
[Windows 64-bit](https://drive.google.com/file/d/1ZCRyhYCYrFM8J7bncQ68OSAH15iQgXnx/view?usp=sharing)  
[Windows 32-bit](https://drive.google.com/file/d/1sokVXgT3msWIA2ez4VfTUolJYjGRMWM9/view?usp=sharing)  
[Mac](https://drive.google.com/file/d/1eWCKfyV48FLE-JOZVT66_4sWZDiC-AMu/view?usp=sharing)  
[Linux](https://drive.google.com/file/d/1D7Mb3NmgJeBeZqo90Q4XrMwyQ54-RxeT/view?usp=sharing)  

---

## NOTES FOR SUB-VERSION 0.0.0-a

[LINK TO THIS SUB-VERSION'S LAST COMMIT](https://github.com/TheLycorisRadiata/game_unity_spoopytreat/tree/440a1a09e6b6f24df655d0fe97c3d8b10f8bf1fa)

![](./ingame_screenshot_0_0_0_a.png)

**Released on the 30th of October 2022**  
**This sub-version respects the school's requirements for the prototype and goes a little bit beyond them.**  

### FEATURES
- The game is made with Unity.
- Installing it on Ubuntu (Linux) is not as straight forward as it seems. The Hub cannot open because it keeps crashing. The solution is to execute the AppImage file with the "--no-sandbox" option.
- Configuring the IDE for Unity.
- Version control has been used in the making of the project. Here is the .gitignore file for Unity: https://github.com/github/gitignore/blob/main/Unity.gitignore
- The game features no more than one scene.
- The player can move with the arrow and WASD keys (or equivalent to WASD as physical keys are used).
- The player can jump with the space bar.
- The player can collide with other objects, and go through others (the small plants).
- The player moves smoothly instead of falling over at the slightest collision.
- The camera, which is not controlled by the user, follows the player in 3rd person and is right behind them.
- A fence with high invisible walls surrounds the map so objects don't fall off it.
- In case an object, the player included, manages to go out of bounds, it is put back onto the map.
- Assets from the Unity asset store are used to compose the scene. The pumpkins weren't facing the right way, so this had to be fixed.
- Moveable objects have different masses.
- The player collects candies to increase their mass, and instead of having the character be slower because of it, I have decided to make their moving speed and jumping force proportional to their mass.
- The player can only collect 3 candies, and the current number they have collected is displayed above the player's head.
- Candies have an idle animation (made not with an actual animation but with code): they have a blue halo, bob up and down, and spin on themselves.
- The scene has 5 candies in total: one is right in front of the player when the game starts to teach them to collect it, one is kept within an enclosure that the player has to jump above to reach, another one is at the end of a small platformer bit, and the last two are inside of cauldrons.
- When a full cauldron is knocked over, a candy is created and the cauldron is replaced by an empty one.
- The game is compiled for Windows 64-bit, Windows 32-bit, Mac and Linux.

### ISSUES
1. An increase in speed makes the player ignore collisions.
2. An increase in speed and/or in mass makes pushing moveable objects more difficult, because the player would then tend to be sent flying.
3. On rare occasions, the cauldrons do not empty even when knocked over.
4. On rarer occasions, the player rotates on the x axis for a bit even without input.

---

## NOTES FOR SUB-VERSION 0.0.0-b

[LINK TO THIS SUB-VERSION'S LAST COMMIT](https://github.com/TheLycorisRadiata/game_unity_spoopytreat/tree/5149900246764ce182620c481d69bc1d849e3c1a)

![](./ingame_screenshot_0_0_0_b.png)

**Released on the 15th of November 2022**  
**This sub-version still respects the school's requirements for the prototype, and since I had time before the deadline I added more features.**  

### ALL PREVIOUS ISSUES HAVE BEEN FIXED
- **ISSUES N°1 AND N°2:** At candy intake, the mass is increased of 2 points instead of 1. The directional speed, rotating speed and jump force still increase at candy intake but in a smaller amount than before. This not only fixes issues n°1 and n°2, it also makes the character more pleasant to maneuver: the increase in mass makes moving objects easier, and the decrease in speed makes the character easier to control. I was also wrong to think that mass has an impact on moving speed, it only does for impulsion, so the jump force. Still, I like the game this way, with the character going faster the more candies they take, so this misconception allowed me to find a nice concept.
- **ISSUE N°3:** The cauldron emptying script is fixed. I was only checking the x rotation axis, and now the z one is checked as well.
- **ISSUE N°4:** The player used to rotate on the y axis randomly, without any input. It has been fixed by freezing the player's y rotation axis. I thought this would prevent input-based rotation, but no, this only freezes it for the physics engine, and the transform can still be updated from code. The y rotation axis of the enemies (= the other big pumpkins) has also been freezed to match the player's logic.

### ADDED FEATURES: APPEARANCE
- The skybox provided with the assets is used. The lighting's color has been made a dark blue to fit.
- Now that the scene is darker, the candies' halo has been made less intense. The halo color has also changed from blue to red.
- The candy counter now displays a 3D candy, and the UI's candy has an halo as well, depending on the number of candies: 0 (nothing), 1 (red), 2 (blue) and 3 (gold).
- The static platforms are arranged differently, and other objects have been added (both fixed and moveable).
- Lights have been added to objects around the map. The candles have a flicker animation and can be extinguished when knocked over.
- Sound effects and sound ambiences are added to the game.

### ADDED FEATURES: PLATFORMER
- The last platform now moves to allow the player to reach the candy at the end of the platformer section.
- The player can move along with the platform without input.
- The player can step to the side with the Q (left) and E (right) keys, or whatever physical equivalent to QWERTY.

### ADDED FEATURES: MAIN MENU
- Options are "Resume Current Game", "New Game", "Options", "Licenses" and "Quit".
- The main menu is a canvas and not another scene. It is navigated with WASD/Q-E/arrow keys and the Enter key. This means that options can be browsed through with an UP/DOWN input, and also with a LEFT/RIGHT one, whichever feels right to the user.
- The main menu is opened first thing, and once in game it can be opened with the Escape key. Using the Escape key again while in the main menu is the same as selecting "Quit", and if the user is in a sub-menu instead, the Escape key brings them back to the main menu.
- The game is paused while the menu is open.
- "Resume Current Game" is hidden and unusable if no game has been started yet.
- "New Game" resets the scene if this wasn't the first game. A boolean is carried through the next iteration of the scene in order to know to load the game immediately instead of awaiting for input in the menu.
- "Options" allows the user to update the sound volumes: General, Music, Ambience and Effects. In this sub-menu, the different options cannot be browsed through with the LEFT/RIGHT input, as these are used to update the volume values. Below those, a "Go Back" option can be selected and validated to go back to the main menu.
- "Licenses" holds URLs that can be selected and opened in the browser, as well as the "Go Back" option.

### ADDED FEATURES: SCREEN MODE
- The F4 key allows to switch between fullscreen and windowed mode. It can be used both in game and in the menu.

### ADDED FEATURES: GAME LOOP
- The game has a physical goal. Once reached and if the player has the required amount of candies, the process waits for a second before sending the user back to the main menu.

### LEVEL DESIGN NOTE
- The UI has a candy counter and it's displayed at all times and not just when a candy is collected. Said counter also displays the maximum amount. This is in order to convey the objective of the game, which is collecting 3 candies, instead of just expressing that items can be collected but we don't know what for.
- The lights are used to guide the player towards the goal, and the flickering of the candles is also to grab their attention.
- The candies' aura is made red, not only to match with the candies themselves because they are actually red, but especially because the environment is mostly green, and red is the color which clashes the most with it. It allows the player to notice the collectibles with more ease.
- The candies slowly bob up and down also in order to make them more noticeable, and also because this movement expresses the idea of a collectible.
- There is a candy right in front of the player when they pop into the scene, but not too close to the player that they would collect it by accident. This is a way to teach the player to collect them. The sound effect when a candy is collected also indicates that the player did a thing, and that this thing is good.
- When three candies are collected, another sound effect is triggered, which indicates a feeling of completion.
- The first candy is a bit high. It is still reachable without a jump, but seeing the candy up in the air might push the player to attempt a jump, teaching them that jumping is indeed possible in this game. There is also a small enclosure containing a candy, which requires a jump in order to get inside of it, but since the enclosure has doors the player may think it requires a key and may not attempt a jump. This is why, worst case, there is a platformer area which definitely requires a jumping ability. The player can then go back to the enclosure and attempt a jump in order to get all three obvious candies.
- Beyond the first candy, there are a few big pumpkins. They are meant to be enemies, but in the meantime they are regular moveable objects. If the player gets curious and reaches them, they may understand that objects can be moved in this game, which is relevant for the cauldrons.
- Cauldrons are candy holders, and the light emitted from them is a subtle indication that something can be done with them, but even if the player doesn't notice that the cauldrons can be knocked over to reveal a candy, three other candies are in the scene so the experience shouldn't be frustrating.

### INPUT NOTE
- It makes sense to use the arrow keys for the menu, but in order to make the handling of the game as intuitive as possible, I have allowed the use of the WASD keys and of the Q/E ones as well. And what's more, these keys are ideal because they are ones that every gamer knows, so even without displaying the controls the user should be able to find the keys. The recent "Physical Keys" Unity feature also allows anyone in the world to use the keys they are familiar with instead of updating the key bindings.
- On commonly used keys, the space bar is indeed used for jumping, the Escape key for the menu/closing/going back, the Enter key to validate or interact, and it is my understanding that F4 is common for the windowed screen mode as well.

### ISSUES
5. Rare wall climbing on invisible walls. A solution is to push the player a bit backwards when they collide with these walls.
6. The camera can clip behind meshes. Cinemachine is a solution.
7. Light sources flicker by themselves. Ghostbusters are busy so gotta call a bakery.

---

## NOTES FOR SUB-VERSION 0.0.0-c

### ADDED FEATURES: GAME LOOP
- Only the player can use the goal. In the current version, the goal still brings the player back to main menu, and in next versions the goal will serve as a portal to another part of the game world.
- The goal requires a certain amount of candies be fed to it to be activated, between 0 and 3.
- The goal's light switches between gray and the candy number's color until it is fed.
- Once fed, the number of candies is taken from the player, and the light's color is constant and a default yellow one.

I'll study Unity properly, from the very beginning, before resuming the development of this game.  

**PLAN FOR THIS SUB-VERSION**
- Fix the previous issues.
- [Maybe] Add a bounce animation when characters move forwards or backwards and have 0 candy. When they have 1 candy or more, they glide like they currently do.
- Add a sound effect when characters jump.
- Add sound effects for when cauldrons are pushed, knocked over, and emptied.
- [Maybe] Add a sound effect when characters move.

