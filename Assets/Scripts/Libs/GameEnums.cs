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
        covered,
        crushed,
        cut,
        decapitated,
        smashed,
        poisoned,
        disproved,
        digested,
        vaporized,
        none
    }
}