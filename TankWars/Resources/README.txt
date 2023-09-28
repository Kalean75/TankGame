Tank Wars Client

Authors:
Devin White
Xuyen Nguyen


Artwork:
Tanks: Devin
Projectiles: Devin
Walls: Devin
Background CS 3500 Staff
Death Animation: Xuyen
Beam Animation: Xuyen


Controls:
W move up
A move Left
S move Down
D move right
Q/Escape Quit
B: Fire Laser
SpaceBar: Fire projectile
R: retromode engaged(if focus in NOT in the drawingpanel)
Left Click: Fire projectile
Right click: Fire Laser

Features/UI elements:
Address Box: The Server Address to connect to
Name BoX: Sets the players name
Connect Button: Connects to server. Must have a valid name entered.
Menu Button: Menu Button
	About: About the game
	Controls: The game controls
	Retro Mode: Invokes the power of the Atari 2600 and a certain game buried in the Nevada desert to bring Ruin to videogame kind



**************************Update***************************************************
Development Log:

11/14/21
1 Commit:
	Inital Setup

11/16/2021
3 Commits:
	Added Member Variables
	Registered Delegates
	Started Ui

11/20/2021
3 Commits:
	Added to the Form
	Fleshed out model and View
	Modified not working Onconnect method

11/21/2021
6 Commits
	Fixed Connection bugs
	Added drawpanel and drawing methods
	Updated drawingPanel
	Started drawing world and walls correctly
	Centered World

11/22/2021
	1 Commit
	Tank drawing updates per frame

11/23/2021
	1 Commit
	Projectile graphics added/drawn

11/24/2021
	2 Commits
	Fixed typos, bug fixes
	Added Drawing methods

11/27/2021
	2 Commits
	Fixed mouse firing bug
	Turret Movement implemented, tank movement improves

11/28/2021
	3 commits
	Events modified
	Lasers implemented
	Movement improved. Cleaned up unncessary code

11/29/2021
	7 Commits
	Started death animations
	Added sprites, changed drawing to draw sprites
	Changfed menu buttom. Added about and control buttons
	Modified HP bar heights for consistency
	Fixed issue where images were loaded on every draw and lagging
	HP bar highlighted with black triangle to look better
	Bug fixes

11/30/2021
	1 commit
	Explosion animation added

12/01/2021
	1 commit
	Comments Added, added retro Graphics button

Features Planned in Future(?):
	Remove retro game button. Make it so Entering a name with Atari in it enables it by default(for the easteregg)
	Show Frame rate
	Allow view size modification
	Improve Sprites
	Improve death animations by making "dead tank" sprite in location after explosion until it respawns.


Design decisions
Uses MVC as per specifications. The methods in the form or drawingpanel deal directly with the UI elements, or otherwise call to gameController in order to handle certain events, such as movement
Movement:
	As movement, and firing weapons is done by sending JSON strings to the server, which then sends JSON back with a new position, the keyPresses set parts of that command to do specific things, IE, w will add "up" to the move section, space
	or the mouseclick will add "fire" etc. This is done every frame, and then fired off to the server after all inputs for that frame have been processed. This is done for readibility, as well as a smoother game experience, as doing this once per frame 
	will make things consistent as JSON data is received.

Visual elements/ The view:
	The majority of the drawings were made personally by me(Devin) though they aren't great, I wanted to do it myself because using the premade assets felt like something of a cop out, so I did the best I could. I let Xuyen pick out a death animation she liked, and we implemented that. The background
	is the only pre-provided element, both because I couldn't think of anything better, and due to the fact that there just wasn't enough time.
	Retro mode as added due to the fact that we already had basic ataro like block drawings that were used for testing while we were implementing methods in the controller, and updating the world. It was simple enough to implement with a button, just trigger a boolean true or false, and then drawing elements
	Modern, or retro, upon the next frame based upon whether the retromode boolean is true or false. This was put in because it was easy, and it's fun.
	Retromode was(is still?) planned as an easteregg that would permanently be enabled if one put "Atari" somewhere in their player name, but time was short so this implementation was used instead.


The controller:
	This handles all the actual gameplay elements, It is responsible for connecting to the server, parsing the JSON received by the server, and sending JSON commands to the server after interpreting the keypress and mouse events fired off within the view
	After parsing the JSON data, the controller then passes off the new World elements(the World,tank, projectiles, powerups, playername, player Id etc) to the view and drawinpanel, which then uses these elements to draw the visuals on a frame by frame basis as needed.

The World:
	This class holds all the data values, primarily dictionaries. It contains the World Size, as well as a dictionary of all the players(tanks), powerups, projectiles, beams, walls, etc that exist in the game as passed from the server to the gamecontroller.
	As of now, the world elements only consist of fields and a constructor, and then the values contained in these are passed to other parts of the program for use.
	it was decided to make a public field in the beam class to hold a timer for the animation frames of the beam drawing. This beam animation element could, and maybe should have been implemented as a seperate internal class in the drawingpanel, but time was short and it was single field in a dictionary used by the drawingpanel anyway.

LivesMode:
	This can be toggled on or off by changing the settings.xml file.
	When LivesMode is on, tanks earn 1 life every 3 scores. If the tank's lives is greater than 0, it doesn't die if its hp gets down to 0. Instead, the tank loses 1 life and its hp restore to maximum.
	Max number of lives: 3.
	Tanks with lives greater than 0 do not survive Beams. If they get hit by a beam, they die and also lose 1 life.

********************************************* Update ***************************************************
Development log:
	December 12, 2021
		7 commits
		Fixed lag issue with turret. Fixed problem with AI where they wouldn't fire
		Added ability to handle powerup spawns from an XML file
		Fixed beam animations. Make tank wrap around world
		Fixed some bugs
		Changed RandomizedPlacement() input argument
		Fixed some bugs
		Fixed issue with dead powerups not being removed

	December 10, 2021 
		2 commits
		Fixed issue where you could still move and fire when dead
		Started on spawning projectiles. Bullets no longer collide with dead tanks. Beams work(but don't draw yet for some reason)
	
	December 9, 2021
		1 commit
		Started on Random spawning

	December 8, 2021
		5 commits
		Set FramePerShot to theWorld.FramesUntilRedrawProj
		Added some variables
		Added bullet collision. Fixed some server issues
		Projectiles now fire Correctly(Timing and collision still needed) Turrets move properly without mouse
		Started on projectiles. Fixed tdir constructor

	December 7, 2021
		1 commit
		Changed tank bdir. Started on collision

	December 5, 2021
		4 commits
		Fixed XML reading for walls. Now works correctly.
		Started xml reading
		Created server.cs, ServerContoller.cs, and Settings. Added methods to start server, receive clients' names, and sending data to clients
		Some setup

Design Decisions

LivesMode:
	This can be toggled on or off by changing the settings.xml file. Using "on" or "off"
	When LivesMode is on, tanks earn 1 life every 3 scores. If the tank's lives is greater than 0, it doesn't die if its hp gets down to 0. Instead, the tank loses 1 life and its hp restore to maximum.
	Max number of lives: 3.
	Tanks with lives greater than 0 do not survive Beams. If they get hit by a beam, they die and also lose 1 life.



	The Game Settings:
		The settings: 
			Universe Size: 2000
			Milliseconds Per Frame: 60
			Tank Respawn Rate: 300
			Maximum Powerups: 4
			Powerup Spawn Rate: 1650

		These settings can be changed from the settings.xml file

		It was decided to make tanks with full beam charges collect and "destroy" powerups, as that was determined to be more fun

		Tank wraparound was decided to simply mimic the sample client, where if the tank touches the edge of the world, it will immediately teleport to the opposite edge. This was simply done by checking if the tanks position was world size/2 in the X and Y direction, or the negative x or y direction within the wrapsaroundworld method, if it did, then it would teleport to the opposite
		edge which was acheived by simply setting the new location to its opposite(depending on if it hit the X or Y edge). This was all done before other movement updates
		
		Projectile collision was achieved in a similar manner, except instead of returning a Vector2D to set the new location, it simply returns a boolean that determines if the projectile is "dead". If the projectile's location hits any edge of the world, it is set to dead and "Destroyed"

		Collision:
			Collision was extremely tricky. The first thing done was to determine tank and wall collision, while there was talk about reusing the same methods, ultimately it was decided to make 2 seperate ones, just in case extra features such as bullets destroying walls, or some other thing was decided at a future date. 2 seperate methods would be easier to alter for such an event

			The other collision methods are within the tank, since other thn the walls, the only other thing that needs rel collision detection is the tank, since the tank needs to lose HP if hit by a projectile, or gain beam charges if hitting a powerup(and thus causing the powerup or projectile to be "Destroyed" as such, these methods were placed in the Tank's "world")

			The last collision detection is the world itself, which is described above.

		Extra Controls:
			It was decided to make B an option to fire the beam due to mouseclicks being less responsive, particularly when there are many other tanks in the world doing things

		Minor improvements to client:
			Turret commands were altered. Before there was a "jitter" when the turret would be moved while the tank was moving. That was changed so now it can move the turret and tnk simultaneously without stopping. Thsi was done by making a new turretdirection vector 2D, updating that in a adjustturret method, then sending that to the sent command every frame
			Added beam animation and death animation to retro-mode. Further improvements on these planned
			Fixed calculation of the "normal modes" beam. It now fires from the tank cannon with far more consistency, and the size accounts for changing world sizes.


	The World:
		Tank and Powerup Placement: The world is divided into a grid of squares. Tanks and powerups spawned into the world are placed in a random square, as long as they do not overlap the walls.
			Issue: The method works fine most of the time. When it was first implemented, we observed some rare occasions where a very small portion of the tank would overlap the wall and get stuck. This bug might have somehow been solved as we worked other problems since we have not run into the issue for many tests(explained more below).
			In addition to the Update method in the servercontroller, there is an update method within the World itself. This is due to so many world variables needing to be checked and updated every frame, it was decided this was a much better, and much quicker method.

		Tank Wraparound: When the tank tries going into a segment of the world's border where there is no walls on both sides of the world, the tank will teleport to the other side as soon as half of it crosses the edge.
	
		Projectile Collision with the World's Edges: When a projectile collides the edge of the world, it is destroyed instead of wrapping around. There was talk about making projectile wraparound a setting, but time ran out, so it wasn't done at this point in time.

	Server Controller:
		This handles the communications with the World and the Client. It starts the update loop where it monitors the changes in the World and implement the changes every frame. It also sends world elements in JSON form to the client,
		receives commands from the clients, and informs the world to update accordingly.

	Major Issues:
		Sample clients and our own clients did not work well together: The sample client appeared laggy and its turret did not spin as smoothly when we connected out client to the same server. The initial diagnosis was that our client was sending too many commands to the server. Due to multiple clients working completely fine if it was ours, OR if it was only theirs.
		We figured out later that we were calling Commands.Clear() within the foreach loop that ran through all commands, which would cause commands to update at an inconsistent rate. Moving that to after the foreach loop fixed the problem. This also had the effect of fixing the AI problems(they sucked way more than before), that we had no idea how to fix, which was a happy coincidence

		The sample client freezes whenever any tank shoots the beams. We have checked the JSON objects that the server was sending as the beams were fired, and they all looked right. We fixed the issue by moving framesUntilFade in Beam.cs to the top and tagged it with [JsonIgnore]. This appears to have fixed it and prelimary testing shows no new bugs.

		Oddities and curiousities:
			There was an issue where when calculating a free space to spawn, a tank would occassionary barely clip the corner of a wall, causing it to be stuck and not able to move, even though it was barely in there. After major testing trying to replicate it, it couldn't be done, whether this is just rare or something was done to fix it is unknown
			Secondly, there sometimes is an issue where the death animation won't play for some deaths before then playing again. This was found to happen only with an extreme amount of tanks dying at once(60 AI clients) and the problem couldn't be found. It also happens rarely and infrequently.

		Other minor issues:
			In some places there appears to be repetition of code, particularly foreach loops. This was done because a better alternative could not be achieved, and performance didn't seem to be affected. There was attempts to change many of these, but the end result in every case ended up performing substantially worse than before the changes, as such,
			we decided to stick to the implementation that worked the best, despite perhaps not being the most beutiful.

	Features Planned in Future(?):
		Add setting for bullet wraparound
		Add setting for destroying walls if hit with enough bullets
		Add setting changing powerup functionality
		Team mode, where teammates can't hurt you.
		The above noted as planned features in above section
		
