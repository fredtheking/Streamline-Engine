using StreamlineEngine.Engine.Pkg;

GameContext context = new GameContext();

context.Init(context);

context.Loop(context);

context.Close();