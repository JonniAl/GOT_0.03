using System.Collections.Generic;

public class InfoDB
{
    public static List<PropertyAction> aProperties = new List<PropertyAction>()
    {
      PropertyAction.Go,
      PropertyAction.Stop,
      PropertyAction.Attack,
      PropertyAction.Patrol
    };

    public static List<PropertyAction> bProperties = new List<PropertyAction>()
    {
      PropertyAction.BuildBarrack,
      PropertyAction.BuildBlacksmith,
      PropertyAction.BuildStables,
      PropertyAction.BuildBank,
      PropertyAction.BuildArcher,
      PropertyAction.BuildSwordman,
      PropertyAction.BuildPeasant,
      PropertyAction.BuildHorseman
    };

    public static List<PropertyAction> rProperties = new List<PropertyAction>()
    {
      PropertyAction.ResearchHorse,
      PropertyAction.ResearchArmor,
      PropertyAction.ResearchSword,
      PropertyAction.ResearchMine
    };

    public enum PropertyAction
    {
        Nothing = 0,

        Go = 1,
        Attack = 2,
        Stop = 3,
        Patrol = 4,

        BuildBarrack = 5,
        BuildBlacksmith = 6,
        BuildStables = 7,
        BuildBank = 8,
        BuildArcher = 9,
        BuildPeasant = 10,
        BuildHorseman = 11,
        BuildSwordman = 12,

        ResearchHorse = 13,
        ResearchArmor = 14,
        ResearchSword = 15,
        ResearchMine = 16
    }

    public enum House
    {
        Stark = 0,
        Lannister = 1
    }

    public enum Scenes
    {
        EndGame = 0,
        MainMenu = 1,
        Level1 = 2,
        Level2 = 3,
        Level3 = 4
    }

    public enum Unit
    {
        Peasant = 0,
        Swordsman = 1,
        Archer = 2,
        Horseman = 3,
        MainBuilding = 4,
        Barraks = 5,
        Stables = 6,
        Bank = 7,
        Blacksmith = 8
    }

    public enum PropertyCells
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven = 11
    }
}