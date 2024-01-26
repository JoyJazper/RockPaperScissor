using System;

namespace RPS.Enums{
     [Serializable]
    public enum RoleType {
        Rock,
        Paper,
        Scissor,
        Lizard,
        Spock,
        None
    }

    [Serializable]
    public enum actions{
        covers,
        crushes,
        cuts,
        decapitates,
        smashes,
        poisons,
        disproves,
        eats,
        vaporizes,
        none
    }
}