using System;
using UnityEngine;

public static class DialogueAssets
{
    public static readonly string[] subtitle_tutorial_initial = { "PLAYER: Just touched down, it’s warmer than I thought it’d be.", "HANDLER: Excellent. Let’s get your systems calibrated.", "Halcian III’s gravitational field is slightly stronger than what your exosuit’s default settings are tuned to.", "We’re gonna need to adjust them to match the planet’s eco-level." };
    public static readonly string gameDisplay_tutorial_1 = "Press 'Q' to bring up your wrist commlink.";
    public static readonly string gameDisplay_tutorial_2 = "Use the scroll wheel to navigate the comlink, and press 'Q' to select an option.";

    public static readonly string[] choice_tutorial_tree_1 = { "Can you adjust them remotely? (Begin Tutorial)", "I’ve done this before, I can calibrate them myself. (Skip Tutorial)" };
    public static readonly string[] subtitle_tutorial_tree_1_c_1 = { "PLAYER: Usually I can do this myself, but I think the storm’s messing with my suit diagnostics. Can you adjust those system settings remotely?" };
    public static readonly string[] subtitle_tutorial_tree_1_c_2 = { "PLAYER: Relax, I’ve done this before. I can calibrate the exosuit systems myself.." };

    public static readonly string[] subtitle_tutorial_pre_choice_1_tree_1 = { "HANDLER: You bet, one moment...", "HANDLER: Okay... you should be all good to go. Try moving around for me."};
    public static readonly string gameDisplay_tutorial_3 = "Use ‘W,’ ‘A,’ ‘S,’ and ‘D’ for movement.";
    public static readonly string[] subtitle_tutorial_post_choice_1_tree_1 = { "HANDLER: Great. All of your motor information is coming in clear on my end. You ready to get to work?" };

    public static readonly string[] choice_tutorial_tree_1_a = { "I think my neck seal is still locked. Can you do anything about that? (Continue Tutorial)", "Yeah let’s do it. (End Tutorial)" };
    public static readonly string[] subtitle_tutorial_tree_1_a_c_1 = { "I think my neck seal is still locked. Can you do something about that?" };
    public static readonly string[] subtitle_tutorial_tree_1_a_c_2 = { "Yeah let’s do it." };

    public static readonly string[] subtitle_tutorial_pre_choice_1_tree_1_a = { "Ahh my bad. Forgot about that one.", "Try looking around...now." };
    public static readonly string gameDisplay_tutorial_4 = "Use the mouse to look around.";
    public static readonly string[] subtitle_tutorial_post_choice_1_tree_1_a = { "Okay. I think I'm all set." };

    public static readonly string[] subtitle_tutorial_end_tree = { "Where am I going?" , "Uh...Relic Site 1 should be just up ahead." };

    // Audio
    public static readonly AudioClip clip_sandstorm = Resources.Load<AudioClip>("sandstorm_folie");
    public static readonly AudioClip clip_intro_ramp = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_01");

    public static readonly AudioClip clip_tutorial_initial_P1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_02");
    public static readonly AudioClip clip_tutorial_initial_H1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_03");
    public static readonly AudioClip clip_tutorial_initial_H2 = Resources.Load<AudioClip>("Scene_01_-_Clip_4a");
    public static readonly AudioClip clip_tutorial_initial_H3 = Resources.Load<AudioClip>("Scene_01_-_Clip_4b");

    // First Set of Choices
    public static readonly AudioClip clip_tutorial_tree_1_choice_1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_05");
    public static readonly AudioClip clip_tutorial_tree_1_choice_2 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_14");

    // Choice One
    public static readonly AudioClip clip_tutorial_tree_1_a_H1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_06");
    public static readonly AudioClip clip_tutorial_tree_1_a_H2 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_07");
    public static readonly AudioClip clip_tutorial_tree_1_a_H3 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_08");

    // Second Set of Choices
    public static readonly AudioClip clip_tutorial_tree_2_choice_1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_09");
    public static readonly AudioClip clip_tutorial_tree_2_choice_2 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_15");


    public static readonly AudioClip clip_tutorial_tree_2_a_H1 = Resources.Load<AudioClip>("Scene_01_-_Clip_10a");
    public static readonly AudioClip clip_tutorial_tree_2_a_H2 = Resources.Load<AudioClip>("Scene_01_-_Clip_10b");
    public static readonly AudioClip clip_tutorial_tree_2_a_P1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_11");

    public static readonly AudioClip clip_tutorial_end_P1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_12");
    public static readonly AudioClip clip_tutorial_end_H1 = Resources.Load<AudioClip>("Remnance_-_Scene_-_01_-_Clip_-_13");
}
