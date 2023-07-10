// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
//using Services;

Console.WriteLine("Hello, World!");



//AppData db = new AppData(@"..\..\..\..\db\lngapp.sqlite");
//ExamWithSessionService service = new ExamWithSessionService(new DbFactory(@"..\..\..\..\db\lngapp.sqlite"));
Dictionary<string, Action<string>> _commands = new Dictionary<string, Action<string>>();

buildCommands();

string cmd = "";


while (!cmd.Equals("exit"))
{
    Console.Write(">");
    cmd = Console.ReadLine();

    try
    {
        executeCommand(cmd);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}


Console.WriteLine("buy");
Console.ReadLine();

void prnObject(object o)
{
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.Indented));
}



void buildCommands()
{
    _commands.Add("start-sess", (prm) =>
    {
        var missionId = int.Parse(prm);
        //service.StartSession(new QuestT1Model { id = missionId });
        //prnObject(service.GetSession(missionId));
    });

    _commands.Add("pass-test", (prm) =>
    {
        var missionId = int.Parse(prm);

        //var q = service.CurrentQuestion(new QuestT1Model { id = missionId});

        bool completed = false;

        while (!completed)
        {
            //Console.Write($"{q.text} = ");
            var sol = Console.ReadLine();
            //var ck = service.CheckSolutionAndNext(new QuestSolution { thoughtId = q.lexemId, solution = sol });

            //if (ck.isCorrect) Console.WriteLine("Nice!");
            //else
            //{
            //    Console.WriteLine("Wrong; Correct is:");

            //    foreach (var item in ck.CorrectStrings)
            //        Console.WriteLine(item);
            //}

            //if(ck.Completed)
            //{
            //    completed = true;
            //    Console.WriteLine("Test is finished");
            //    continue;
            //}

            //q = service.CurrentQuestion(new QuestT1Model { id = missionId });
        }

    });
}

void executeCommand(string str)
{
    var cmds = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (_commands.ContainsKey(cmds[0]))
    {
        _commands[cmds[0]](cmds[1]);
    }
    else
        Console.WriteLine($"`{cmds[0]}` is not recognized");
}