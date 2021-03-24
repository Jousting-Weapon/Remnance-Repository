using System;
using UnityEngine;

public static class DialogueAssets
{
    #region Tutorial
    public static readonly string[] subtitle_tutorial_initial = { "PLAYER: Just touched down, it’s warmer than I thought it’d be.", "HANDLER: Excellent. Let’s get your systems calibrated.", "Halcian III’s gravitational field is slightly stronger than what your exosuit’s default settings are tuned to.", "We’re gonna need to adjust them to match the planet’s eco-level." };
    public static readonly string gameDisplay_tutorial_1 = "Press 'Q' to bring up your wrist commlink.";
    public static readonly string gameDisplay_tutorial_2 = "Use the scroll wheel to navigate the comlink, and press 'Q' to select an option.";

    public static readonly string[] choice_tutorial_tree_1 = { "Can you adjust them remotely? (Begin Tutorial)", "I’ve done this before, I can calibrate them myself. (Skip Tutorial)" };
    public static readonly string[] subtitle_tutorial_tree_1_c_1 = { "PLAYER: Usually I can do this myself, but I think the storm’s messing with my suit diagnostics. Can you adjust those system settings remotely?" };
    public static readonly string[] subtitle_tutorial_tree_1_c_2 = { "PLAYER: Relax, I’ve done this before. I can calibrate the exosuit systems by myself." };

    public static readonly string[] subtitle_tutorial_pre_choice_1_tree_1 = { "HANDLER: You bet, one moment...", "HANDLER: Okay... you should be all good to go. Try moving around for me."};
    public static readonly string gameDisplay_tutorial_3 = "Use ‘W,’ ‘A,’ ‘S,’ and ‘D’ for movement.";
    public static readonly string[] subtitle_tutorial_post_choice_1_tree_1 = { "HANDLER: Great. All of your motor information is coming in clear on my end. You ready to get to work?" };

    public static readonly string[] choice_tutorial_tree_1_a = { "I think my neck seal is still locked. Can you do anything about that? (Continue Tutorial)", "Yeah let’s do it. (End Tutorial)" };
    public static readonly string[] subtitle_tutorial_tree_1_a_c_1 = { "PLAYER: I think my neck seal is still locked. Can you do something about that?" };
    public static readonly string[] subtitle_tutorial_tree_1_a_c_2 = { "PLAYER: Yeah let’s do it." };

    public static readonly string[] subtitle_tutorial_pre_choice_1_tree_1_a = { "HANDLER: Ahh my bad. Forgot about that one.", "HANDLER: Try looking around...now." };
    public static readonly string gameDisplay_tutorial_4 = "Use the mouse to look around.";
    public static readonly string[] subtitle_tutorial_post_choice_1_tree_1_a = { "PLAYER: Okay. I think I'm all set." };

    public static readonly string[] subtitle_tutorial_end_tree = { "PLAYER: Where am I going?", "HANDLER: Uh...Relic Site 1 should be just up ahead." };

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
    #endregion

    #region SiteEntrance
    public static readonly string[] subtitle_entrance_1 = { "PLAYER: Alright...I think I've reached RS1.", "HANDLER: Great." };
    public static readonly string[] subtitle_entrance_2 = { "PLAYER: This place looks far worse than what those preliminary photos depicted.", "PLAYER: Did we ever recover those probes?", "HANDLER: That...that is a great question.  I...I have no idea." };
    public static readonly string[] subtitle_entrance_3 = { "PLAYER: You're in a state-of-the-art relay booth and you can't tell me if a few probes are still online?", "HANDLER: Well I-", "PLAYER: You know what, it doesn't even matter. Do we have any idea where the artifact is located?" };
    public static readonly string[] subtitle_entrance_4 = { "HANDLER: Yes! Well...I can give you a general location at the very least.", "HANDLER: Site 1 is completely isolated from Sites 2 and 3. You see those mountains around you?" };
    public static readonly string[] subtitle_entrance_5 = { "PLAYER: ...I'm in the middle of a sandstorm, I can't see 20 feet in front of me, let alone some mountains 'in the distance.'", "HANDLER: Right. Forgot about that. Well suffice to say, the artifact is somewhere in this valley." };
    public static readonly string[] subtitle_entrance_6 = { "PLAYER: Great. Thanks for all your help.", "HANDLER: Anytime. You know where to find me." };
    public static readonly string gameDisplay_entrance_1 = "Periodically, opening your wrist commlink ('Q') will allow you to ask your Handler questions.";

    // Audio
    public static readonly AudioClip clip_entrance_1 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_1");
    public static readonly AudioClip clip_entrance_2 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_2");
    public static readonly AudioClip clip_entrance_3 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_3");
    public static readonly AudioClip clip_entrance_4 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_4");
    public static readonly AudioClip clip_entrance_5 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_5");
    public static readonly AudioClip clip_entrance_6 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_6");
    public static readonly AudioClip clip_entrance_7 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_7");
    public static readonly AudioClip clip_entrance_8 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_8");
    public static readonly AudioClip clip_entrance_9 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_9");
    public static readonly AudioClip clip_entrance_10 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_10");
    public static readonly AudioClip clip_entrance_11 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_11");
    public static readonly AudioClip clip_entrance_12 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_12");
    public static readonly AudioClip clip_entrance_13 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_13");
    public static readonly AudioClip clip_entrance_14 = Resources.Load<AudioClip>("Remnance_Scene_2_Clip_14");
    #endregion

    #region Exploration
    public static readonly string[] choice_exploration_tree_1 = { "Relic Guardians?", "Can I document these buildings?" };

    public static readonly string[] subtitle_exploration_tree_1_c1 = { "PLAYER: The briefing said there were no other sentients on Halcian III. What are those things?", "HANDLER: The briefing said there were no other organics on Halcian III. Those \"robots\" that you're either hearing or seeing float around, those are Relic Guardians", "HANDLER: I'm surprised you haven't seen any on other assignments. One of the teams on H2 said they ran into a ton of them." };
    public static readonly string[] choice_exploration_tree_2 = { "Are they dangerous?", "Is there any way to shut them down?" };
    public static readonly string[] subtitle_exploration_tree_2_c1 = { "PLAYER: Are they...dangerous?", "HANDLER: Oh absolutely. That team on H2 said they ran into them. As in past tense.", "HANDLER: There's a reason I'm not down there with you. Sorry. I probably should have mentioned that earlier.", "PLAYER: Ya think?" };
    public static readonly string[] subtitle_exploration_tree_2_c2_a = { "PLAYER: Is there anyway to shut them down or disable them?", "HANDLER: With what, your camera? Unless you're carrying some contraband I don't know about, which is saying something because I have access to literally every piece of equipment you're wearing, that sounds like a terrible plan." };
    public static readonly string[] subtitle_exploration_tree_2_c2_b = { "PLAYER: I don't see you coming up with any better ideas.", "HANDLER: Well, I'm not the one down there, unarmed mind you, trying to pick a fight with a bunch of alien robots.", "PLAYER: Evidently." };

    public static readonly string[] subtitle_exploration_tree_1_c2_a = { "PLAYER: What's S.A.L.V.A.G.E.'s policy on building documentation?", "HANDLER: Building documentation? As in taking pictures of run down houses?" };
    public static readonly string[] subtitle_exploration_tree_1_c2_b = { "PLAYER: Yeah. Some of these structures may have been here for decades. Between the sharp winds and these sandstorms, I'm surprised they're not already buried.", "PLAYER: A few years ago, during one of my assignments in the Asar System, we were able to catalog nearly every building with a structural integrity above 30%.", "PLAYER: By the time the mission was over, we had begun to discern patterns between the layouts of all the buildings.", "PLAYER: Larger structures, likely used for sanitation, had been placed away from the smaller houses that we assumed were for residential purposes.", "PLAYER: Through just documenting the structures, our team concluded that this civilization had mastered zoning. Pretty neat right?" };
    public static readonly string[] subtitle_exploration_tree_1_c2_c = { "HANDLER: Our job, your job is to make sure the Archive gets a photo of that artifact. If you wanna take crazy pictures of old buildings, be my guest." };

    // Audio
    public static readonly AudioClip clip_exploration_tree_1_choice_1 = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_1");
    public static readonly AudioClip clip_exploration_tree_1_c1_a= Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_2a");
    public static readonly AudioClip clip_exploration_tree_1_c1_b = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_2b");

    public static readonly AudioClip clip_exploration_tree_1_choice_2 = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_11");
    public static readonly AudioClip clip_exploration_tree_1_c2_a = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_12");
    public static readonly AudioClip clip_exploration_tree_1_c2_b = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_13a");
    public static readonly AudioClip clip_exploration_tree_1_c2_c = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_13b");
    public static readonly AudioClip clip_exploration_tree_1_c2_d = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_13c");
    public static readonly AudioClip clip_exploration_tree_1_c2_e = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_13d");
    public static readonly AudioClip clip_exploration_tree_1_c2_f = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_13e");
    public static readonly AudioClip clip_exploration_tree_1_c2_g = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_14");

    public static readonly AudioClip clip_exploration_tree_2_choice_1 = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_3");
    public static readonly AudioClip clip_exploration_tree_2_c1_a = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_4a");
    public static readonly AudioClip clip_exploration_tree_2_c1_b = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_4b");
    public static readonly AudioClip clip_exploration_tree_2_c1_c = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_5");

    public static readonly AudioClip clip_exploration_tree_2_choice_2 = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_6");
    public static readonly AudioClip clip_exploration_tree_2_c2_a = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_7");
    public static readonly AudioClip clip_exploration_tree_2_c2_b = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_8");
    public static readonly AudioClip clip_exploration_tree_2_c2_c = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_9");
    public static readonly AudioClip clip_exploration_tree_2_c2_d = Resources.Load<AudioClip>("Scene3/EX_Scenes_Clip_10");
    #endregion
}
