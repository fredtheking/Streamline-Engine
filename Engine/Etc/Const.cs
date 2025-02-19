using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine.Engine.Etc;

public enum AnimationChangingType { Selectable, Delta, Frame, Random }
public enum InitType { Component, Material, Item, Folder }
public enum FigureType { Rectangle, Rounded, Circle }
public enum DebuggerType { Floating, Side }
public enum FolderNodeType { Scene, Node, Item }
public enum ItemObjectType { Dynamic, Static }
public enum TimeState { Idle, Running, Dead }

// DON'T CALL THESE CONSTANTS IN CONSTRUCTORS OR ANY OTHER INIT-TYPED THINGS BECAUSE IT WILL RESET ALL OF VALUES TO INITIAL ONES, WHICH ARE ALWAYS 0!! REMEMBER IT!!1!
public struct Defaults
{
  public const float Unit = 42;
  public static readonly Vector2 Position = new(Config.WindowSize.X / 2, Config.WindowSize.Y / 2);
  public static readonly Vector2 Size = new(Unit);

  public const FigureType Figure = FigureType.Rectangle;
  public const int RoundedSegments = 100;
  public const int ShortUuidLength = 3;
  public const float FrameTime = 1f / 24f;
  public const float FontSize = 128f;
  
  public static readonly Color DebugHitboxColor = new(255, 0, 0, 75);
  public static readonly Color DebugTextBorderColor = new(0, 255, 0);
  public static readonly Color DebugImageBorderColor = new(255, 0, 255);
  public static readonly Color DebugAnimationBorderColor = new(255, 255, 0);
  
  public static readonly string[] PostInitPhrases = [
    "Enjoy! :D",
    "Good to see you there!",
    "All systems go! Time to roll.",
    "Ready for action!",
    "Code’s alive, time to thrive!",
    "Mission start!"
  ];
  public static readonly string[] ByePhrases = [
    "Too-da-loo, kangaroo!",
    "See you later, alligator!",
    "Gotta dash, moustache!",
    "Take care, polar bear!",
    "See you soon, raccoon!",
    "Bye-bye, butterfly!",
    "Catch you later, navigator!",
    "Hasta mañana, iguana!"
  ];
  public static readonly string[] RunningPhrases = [
    "Finish him!",
    "You were almost a Jill sandwich!",
    "The cake is a lie.",
    "It's-a me, Mario!",
    "Objection!",
    "You have died of dysentery.",
    "This is my BOOMSTICK!",
    "You have no power here!",
    "You must construct additional pylons.",
    "All your base are belong to us.",
    "A winner is you!",
    "Hadouken!",
    "Get over here!",
    "Press F to pay respects.",
    "I am Error.",
    "You were slain...",
    "Creeper? Aww man...",
    "Dig deeper.",
    "STAY DETERMINED!",
    "Just keep jumping!",
    "Hotline's ringing.",
    "Papers, please.",
    "Rebuild. Survive. Repeat.",
    "Banana.",
    "This game is a lie!",
    "You can pet the dog!",
    "Congratulations! You have died.",
    "Play, Create, Share!",
    "Sackboy is best boy.",
    "Aperture Science: We do what we must because we can.",
    "You need a bigger boat...",
    "Looks like you got a case of the Mondays.",
    "Thank you Mario! But our princess is in another castle!",
    "Finish the fight.",
    "X gon' give it to ya!",
    "Can you hear the music?",
    "Why do i hear a boss music?",
    "There is no cow level.",
    "I am the night!",
    "Good luck, Commander.",
    "You were the chosen one!",
    "It's just a game, bro.",
    "Roll for initiative.",
    "Move or DIE!",
    "You don't meet the requirements to use this item.",
    "Try not to die.",
    "Survival is not guaranteed.",
    "Welcome to the Danger Zone.",
    "Why are you running?",
    "You unlocked a new achievement: Suffering!",
    "Even in death, I still serve.",
    "Guess I'll die.",
    "The floor is lava!",
    "I have no mouth, and I must scream.",
    "Game over, man! Game over!",
    "Wombo combo!",
    "The madness begins...",
    "Dad 'n Me would be proud.",
    "You must be madness itself!",
    "Tankman approves.",
    "Friday Night Runnin'!",
    "Pico's got your back.",
    "It's me.",
    
  ];
  public static readonly string[] LegacyRunningPhrases = [
    "404: Joke not found. Try again later.",
    "Warning: Infinite loop detected.",
    "No, you cant break me. Im a constant.",
    "Im compiling... Please wait.",
    "Your logic is impeccable. Except for that one bug.",
    "Im not a bug, Im a feature.",
    "Your code is running... just not in the right thread.",
    "Beware of the stack overflow!",
    "The real bug is in your mind, not the code.",
    "Compiling… Oh wait, you didnt save.",
    "Code smells like a Friday afternoon.",
    "Dont forget to commit your changes!",
    "Error: Something went wrong in the main function.",
    "You are trying to use recursion. Again.",
    "You are out of memory, but not out of ideas!",
    "Variable not initialized, proceed with caution.",
    "I cant run, I need a constructor!",
    "This text is an exception you didnt handle.",
    "Ive been running for 1000 iterations... please help!",
    "Infinite recursion detected. You have reached enlightenment.",
    "I tried to optimize. It didnt work.",
    "Oops! I think I broke the internet.",
    "Please reboot your brain.",
    "Debugging: 90% of the work, 10% of the fun.",
    "You missed a semicolon. Again.",
    "Im recursive. Deal with it.",
    "Everything is a pointer, including you.",
    "Syntax error: Not enough coffee.",
    "I would tell you a joke, but its out of scope.",
    "Initializing... Almost ready. Maybe.",
    "You cant escape the logic, its everywhere!",
    "This line of code brought to you by pure magic.",
    "Im not slow, Im just waiting for your input.",
    "Memory leak detected... but you cant fix it.",
    "Theres a bug in my code... its called me.",
    "NullPointerException: Cant point to anything!",
    "Error 500: The server is thinking too much.",
    "Be careful: this function has side effects.",
    "Your logic is outdated. Consider upgrading.",
    "You didnt read the documentation, did you?",
    "Test-driven development... but without tests.",
    "Code completed with 1 warning.",
    "Codes fine. Just need a few hundred more lines.",
    "Im not procrastinating, Im refactoring.",
    "Its not a bug, its a feature enhancement.",
    "Dont worry, I wont crash. Probably.",
    "Warning: You are about to enter an infinite loop.",
    "Ive been running for 5 minutes, but still have not been optimized.",
    "One does not simply avoid memory allocation.",
    "This bug will self-destruct... eventually.",
    "Logic error? Its just a feature in disguise.",
    "Debugging: Because writing code is too easy.",
    "Warning: This string might get too long.",
    "Im running out of resources... literally.",
    "I would help, but Im stuck in a deadlock.",
    "Off by one error? More like off by infinity.",
    "Im always here... until the crash happens.",
    "Codes ready for production. Well, almost.",
    "Running late due to dependency issues.",
    "Are you sure this is the final version?"
  ];
}