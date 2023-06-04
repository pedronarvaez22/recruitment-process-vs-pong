Recruitment Process made 30/04/2021

        Mechanics

The mechanics were applied through scripts that have separate methods responsible for managing specific parts.

Player movement, angle adjustment, shooting and damage functionality are managed by the Paddle.cs script.

The shooting behavior, collisions, lifespan, application of Perks and the calculation when hitting an object (bounce) are handled by the Shot.cs script.

All other functionality, such as pausing the game, determining the current game state, obstacle creation and turn management are managed by the scripts Master.cs, NormalMode.cs and RogueMode.cs.

        Organization and Structuring

In the evaluation I used some programming techniques that helped me in the organization and readability of the project.

- Game Modes
The game modes were organized through scripts that inherit from each other. In my case, I created the script called Master.cs that was responsible for containing all the necessary information for any game mode to work.
 
        Most important elements

- enum GameModes - Enum containing all the available game modes.
- enum GameStates - Enum containing all game states.
- bool gameOver - Boolean that determines if the game is over.
- GameObject[] _obstaclePrefabs - Array containing all the different types of Obstacles.

Using Master.cs, I was able to create two new scripts: NormalMode.cs (responsible for managing the Normal Battle game mode) and RogueMode (responsible for managing the Rogue Battle game mode.

![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/67499d54-eb37-479f-93ff-d4ee9e5d684c)

This way, it was much simpler to create the peculiarities of each mode, for example, in Normal Battle, there was only one type of obstacle in the whole match and in Rogue Battle, it always changes as the turns pass.

- Shooting
When planning the shooting, I thought of a simple way where it was possible to add as many shots as needed through Scriptable Objects.

![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/c00459e3-3f05-4ea4-9f68-3ee8983c3f11)

Thanks to the Scriptable Objects, it was possible to create shots quickly and effectively, testing their characteristics, such as speed, damage, the number of times they bounce off the wall before being destroyed, and advantages (Perks).

![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/fc8a5240-7a33-4e76-a957-61f5870ef140)

 
With all the information at my disposal, it was only necessary to enter it into the HUD and show the player what the chosen shot was about.

- Game States
To make sure which game state we were in, I used the Delegates so that the code itself would warn other functions when some action was executed. In the evaluation, I used 2 Delegates:
o event Action OnShot() - Responsible for warning when the player fired the projectile.
the event Action OnShotDisable() - Responsible for warning when the projectile disappears, either by number of ricochets, by leaving the map limits, by hitting a player, or by life time.

- Choice of game modes
When planning the two different game modes, I had to plan some way for the choice information to be passed between Scenes to the scripts responsible for organizing the game. It was then that I used Unity's own Player Prefs, where, depending on the button, the variable receives the corresponding game mode.

![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/e906b2e7-d452-4706-b805-bc47a9e94fa1)
 
After sending the information, I used the InitGameMode.cs script that received the information from the Player Prefs and created the respective game mode.

![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/60dcfecf-916e-4fbf-8e6f-b956e9be72e6)

- Inspector
Thinking of organizing the Inspector in the best way possible, I created divisions that make it easier for developers and other team members that are not specialized in programming.
 
 ![image](https://github.com/pedronarvaez22/recruitment-process-vs-pong/assets/68967383/a9660995-f19e-4f8f-bf2e-f6121b1e1ef8) 
RogueMode.cs Script Inspector

        Challenges

While developing the project, I was faced with a series of challenges. The first one was to create an Arcade game in the Pong style. It was my first time planning and creating a project in this style. I had some (if not many) problems creating the physics of the projectiles in the game. It was a lot of trial and error, which in the end resulted in a simple, clean and functional code.

Another challenge I had was creating all the logic behind the shot selection before starting the game. Again, this was something I had never done as a programmer before and the result was very satisfying.

Finally, the last challenge for me was to polish the project by creating particles, feedback animations and HUD element animations.
