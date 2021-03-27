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

    #region Cave
    public static readonly string[] subtitle_cave_1 = { "PLAYER: Whoa...what's this?", "HANDLER: Uhh...wait. Where are you? I just lost visual.", "PLAYER: Check your topographical telemetry genius, I just entered some sort of cave system." };
    public static readonly string[] subtitle_cave_2 = { "HANDLER: Pssh, I knew that. You think the artifact's in here?", "PLAYER: Yeah I do.", "PLAYER: The briefing said the artifact was relatively preserved.", "PLAYER: With the sandstorm going on outside I figured it would be in a building. This makes much more sense." };

    public static readonly string[] choice_cave_tree_1 = { "Are there Relic Guardians in here?", "Have you ever been in the field?" };

    public static readonly string[] subtitle_cave_tree_1_c1 = { "PLAYER: Any idea if those Relic Guardians are in here?", "HANDLER: No idea. And until the interference from this sandstorm goes away I'm not gonna be able to tell you.", "PLAYER: Okay. Very helpful as always." };
    public static readonly string[] subtitle_cave_tree_1_c2a = { "PLAYER: Have you ever been in the field?", "HANDLER: Hahaha...wait, are you being serious right now?", "PLAYER: I was trying to be.", "HANDLER: Oh. Well in that case, no. No I've never been deployed before." };
    public static readonly string[] subtitle_cave_tree_1_c2b = { "HANDLER: The 'dream' of being an overworked Trekker was lost on me.", "PLAYER: Ouch.", "HANDLER: Hey, you asked. Besides, I get paid just as much as you do, and I don't ever have to set foot on some dusty planet.", "PLAYER: So you're only in it for the credits.", "HANDLER: Are you not?" };

    // Audio
    public static readonly AudioClip clip_cave_1 = Resources.Load<AudioClip>("CaveScene/PHA_Clip_01");
    public static readonly AudioClip clip_cave_2 = Resources.Load<AudioClip>("CaveScene/PHA_Clip_02");
    public static readonly AudioClip clip_cave_3 = Resources.Load<AudioClip>("CaveScene/PHA_Clip_03");
    public static readonly AudioClip clip_cave_4 = Resources.Load<AudioClip>("CaveScene/PHA_Clip_04");
    public static readonly AudioClip clip_cave_5a = Resources.Load<AudioClip>("CaveScene/PHA_Clip_05a");
    public static readonly AudioClip clip_cave_5b = Resources.Load<AudioClip>("CaveScene/PHA_Clip_05b");
    public static readonly AudioClip clip_cave_5c = Resources.Load<AudioClip>("CaveScene/PHA_Clip_05c");

    public static readonly AudioClip clip_cave_tree_1_choice_1a = Resources.Load<AudioClip>("CaveScene/PHA_Clip_6");
    public static readonly AudioClip clip_cave_tree_1_choice_1b = Resources.Load<AudioClip>("CaveScene/PHA_Clip_7");
    public static readonly AudioClip clip_cave_tree_1_choice_1c = Resources.Load<AudioClip>("CaveScene/PHA_Clip_8");

    public static readonly AudioClip clip_cave_tree_1_choice_2a = Resources.Load<AudioClip>("CaveScene/PHA_Clip_9");
    public static readonly AudioClip clip_cave_tree_1_choice_2b = Resources.Load<AudioClip>("CaveScene/PHA_Clip_10");
    public static readonly AudioClip clip_cave_tree_1_choice_2c = Resources.Load<AudioClip>("CaveScene/PHA_Clip_11");
    public static readonly AudioClip clip_cave_tree_1_choice_2d = Resources.Load<AudioClip>("CaveScene/PHA_Clip_12a");
    public static readonly AudioClip clip_cave_tree_1_choice_2e = Resources.Load<AudioClip>("CaveScene/PHA_Clip_12b");
    public static readonly AudioClip clip_cave_tree_1_choice_2f = Resources.Load<AudioClip>("CaveScene/PHA_Clip_13");
    public static readonly AudioClip clip_cave_tree_1_choice_2g = Resources.Load<AudioClip>("CaveScene/PHA_Clip_14");
    public static readonly AudioClip clip_cave_tree_1_choice_2h = Resources.Load<AudioClip>("CaveScene/PHA_Clip_15");
    public static readonly AudioClip clip_cave_tree_1_choice_2i = Resources.Load<AudioClip>("CaveScene/PHA_Clip_16");
    #endregion

    #region Artifact
    public static readonly string[] subtitle_artifact_initial_1 = { "PLAYER: I took a picture and documented the artifact. We should be all good to go.", "HANDLER: Excellent. Management also wants you to take the artifact. We need it for some sort of testing." };
    public static readonly string[] subtitle_artifact_initial_2 = { "PLAYER: What?!? My job is to document this place." , "PLAYER: If we start taking pieces of this culture's ancestry we're no better than whatever forces ended them in the first place." };
    public static readonly string[] subtitle_artifact_initial_3 = { "HANDLER: Get off your high horse, no one's going to miss this.", "HANDLER: Management is hell bent on making sure that we take whatever that thing is back with us." };
    public static readonly string[] subtitle_artifact_initial_4 = { "HANDLER: As far as their concerned it'll fund this entire mission.", "HANDLER: Just make this easy on both of us and take the damn thing." };

    public static readonly string[] choice_artifact_tree_1 = { "Not gonna happen.", "Fine." };
    public static readonly string[] subtitle_artifact_tree_1_c_1 = { "PLAYER: Not gonna happen." };
    public static readonly string[] subtitle_artifact_tree_1_c_2 = { "PLAYER: Fine. But I'm not happy about this." };

    public static readonly string[] subtitle_artifact_tree_1_c1_a = { "HANDLER: Okay well let me tell you how this is gonna go then. I'm the only one who has startup access to your lander. Ergo, you can't leave without me.", "HANDLER: Now I hate to play the bad guy, but both of our asses are on the line if you, yes you, don't pick up that old relic." };
    public static readonly string[] subtitle_artifact_tree_1_c2_a = { "HANDLER: You don't have to be happy about it, just grab it." };
    public static readonly string[] substitle_artifact_tree_1_c2_b = { "PLAYER: Okay, I have it." };

    public static readonly string[] choice_artifact_tree_2 = { "Pretend to take Artifact", "Take Artifact" };
    public static readonly string[] subtitle_artifact_tree_2 = { "PLAYER: I have the artifact." };

    public static readonly string[] subtitle_artifact_retrieved = { "HANDLER: Greaaat. Uh...Well the sandstorm let up.", "PLAYER: Okay...", "HANDLER: Do you want the good news or the bad news?" };

    public static readonly string[] choice_artifact_tree_3 = { "Good news", "Bad news" };
    public static readonly string[] subtitle_artifact_tree_3_c_1 = { "PLAYER: Give me the good news." };
    public static readonly string[] subtitle_artifact_tree_3_c_2 = { "PLAYER: Give me the bad news." };

    public static readonly string[] subtitle_artifact_tree_3_no_good_news = { "HANDLER: Uh...there is no good news." };
    public static readonly string[] subtitle_artifact_tree_3_bad_news = { "HANDLER: Okay, well the bad news is that the sandstorm is definitely still going strong.", "HANDLER: To make things worse, those Relic Guardians that I mentioned earlier...well I think they're converging on your location.", "HANDLER: You need to take the artifact and get back to the lander now." };

    public static readonly string[] subtitle_artifact_tree_3_c_1_a = { "PLAYER: Then why did you ask?" };
    public static readonly string[] subtitle_artifact_tree_3_c_2_a = { "PLAYER: And the good news?" };

    public static readonly string[] subtitle_artifact_tree_3_on_the_way = { "PLAYER: On my way." };

    // Audio
    public static readonly AudioClip clip_artifact_1 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_17");
    public static readonly AudioClip clip_artifact_2 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_18");
    public static readonly AudioClip clip_artifact_3 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_19a");
    public static readonly AudioClip clip_artifact_4 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_19b");
    public static readonly AudioClip clip_artifact_5 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_20a");
    public static readonly AudioClip clip_artifact_6 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_20b");
    public static readonly AudioClip clip_artifact_7 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_20c");
    public static readonly AudioClip clip_artifact_8 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_20d");

    public static readonly AudioClip clip_artifact_t1_c1 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_21");
    public static readonly AudioClip clip_artifact_t1_c1_a = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_22a");
    public static readonly AudioClip clip_artifact_t1_c1_b = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_22b");

    public static readonly AudioClip clip_artifact_t2 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_23");

    public static readonly AudioClip clip_artifact_t1_c2 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_24");
    public static readonly AudioClip clip_artifact_t1_c2_a = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_25");
    public static readonly AudioClip clip_artifact_t1_c2_b = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_26");

    public static readonly AudioClip clip_artifact_retrieved_1 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_27");
    public static readonly AudioClip clip_artifact_retrieved_2 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_28");
    public static readonly AudioClip clip_artifact_retrieved_3 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_29");

    public static readonly AudioClip clip_artifact_t3_c1 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_30");
    public static readonly AudioClip clip_artifact_t3_c1_a = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_32");

    public static readonly AudioClip clip_artifact_no_good_news = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_31");
    public static readonly AudioClip clip_artifact_bad_news_a = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_33a");
    public static readonly AudioClip clip_artifact_bad_news_b = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_33b");
    public static readonly AudioClip clip_artifact_bad_news_c = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_33c");
    public static readonly AudioClip clip_artifact_on_the_way = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_34");

    public static readonly AudioClip clip_artifact_t3_c2 = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_35");
    public static readonly AudioClip clip_artifact_t3_c2_a = Resources.Load<AudioClip>("ArtifactScene/PHA_Clip_36");
    #endregion
}
