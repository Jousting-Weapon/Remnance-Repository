using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueAssets
{
    public static readonly string[] tutorial_subtitle_1 = { "PLAYER: Just touched down, it’s warmer than I thought it’d be.", "HANDLER: Excellent. Let’s get your systems calibrated.", "Halcian III’s gravitational field is slightly stronger than what your exosuit’s default settings are tuned to.", "We’re gonna need to adjust them to match the planet’s eco-level." };
    public static readonly string[] tutorial_gameDisplay_1 = { "Press 'q' to bring up your wrist comlink.", "Use the ‘up’ and ‘down’ arrow keys to navigate the menu." };

    public static readonly string[] tutorial_c1a_tree = { "Can you adjust them remotely? (Begin Tutorial)", "I’ve done this before, I can calibrate them myself. (Skip Tutorial)" };
    public static readonly string[] tutorial_c1a_subtitles = { "PLAYER: Okay, great. Can you adjust them remotely?", "PLAYER: I’ve done this before, I can calibrate them myself." };

    // If option 1 is taken go to TREE 2. If option 2 is taken go to tutorial_c1b_tree

    // TREE 2
    public static readonly string[] tutorial_c1a_tree_2_subtitles = { "HANDLER: You bet, one moment...", "HANDLER: Okay you should be all good to go. Try moving around for me.", "HANDLER: Great. All of your motor information is coming in clear on my end. You ready to get to work?", };
    public static readonly string[] tutorial_gameDisplay_2 = { "Use ‘W,’ ‘A,’ ‘S,’ and ‘D’ for movement." };

    public static readonly string[] tutorial_tree_2 = { "My neck seal is still locked. (Continue Tutorial).", "Yeah let’s do it. (End Tutorial)" };
    public static readonly string[] tutorial_c2a_subtitles = { "Uh...I think my neck seal is still locked. Anything you can do about that?" };
    public static readonly string[] c2a_tree_subtitles = { "Ahh my bad. Forgot about that one.", "Try looking around...now.", "Okay. I think I'm all set." };

    public static readonly string[] tutorial_c2b_subtitles = { "Yeah let’s do it." };

    // c1b_tree - Last message in tutorial
    public static readonly string[] tutorial_c1b_tree = { "Where am I going?" };


    public static readonly AudioClip tutorial_clip_P1 = Resources.Load<AudioClip>("Intro_Subtitle_P1");
    public static readonly AudioClip tutorial_clip_H1 = Resources.Load<AudioClip>("Intro_Subtitle_H1");
    public static readonly AudioClip tutorial_clip_H2 = Resources.Load<AudioClip>("Intro_Subtitle_H2");
    public static readonly AudioClip tutorial_clip_H3 = Resources.Load<AudioClip>("Intro_Subtitle_H3");

    // If C1a is taken
    public static readonly AudioClip tutorial_clip_C1a = Resources.Load<AudioClip>("C1a_Audio");
    public static readonly AudioClip C1a_Tree_H1 = Resources.Load<AudioClip>("C1a_Tree_H1");
    public static readonly AudioClip C1a_Tree_H2 = Resources.Load<AudioClip>("C1a_Tree_H2");
    public static readonly AudioClip C1a_Tree_H3 = Resources.Load<AudioClip>("C1a_Tree_H3");

    // If C2a is taken
    public static readonly AudioClip clip_C2a_Audio = Resources.Load<AudioClip>("C2a_Audio");

    public static readonly AudioClip clip_C2a_Tree_H1 = Resources.Load<AudioClip>("C2a_Tree_H1");
    public static readonly AudioClip clip_C2a_Tree_H2 = Resources.Load<AudioClip>("C2a_Tree_H2");
    public static readonly AudioClip clip_C2a_Tree_P1 = Resources.Load<AudioClip>("C2a_Tree_P1");

    //If C2b is taken
    public static readonly AudioClip clip_C2b_Audio = Resources.Load<AudioClip>("C2b_Audio");

    // If C1b is taken
    public static readonly AudioClip tutorial_clip_C1b = Resources.Load<AudioClip>("C1b_Audio");
    public static readonly AudioClip tutorial_clip_C1b_Tree_P1 = Resources.Load<AudioClip>("C1b_Tree_P1");
}
