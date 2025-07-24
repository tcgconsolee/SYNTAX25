using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class DIALOGUES : MonoBehaviour
{
    public TMP_Text captions;
    public bool started = false;
    public static RaycastHit2D diahit;
    public AudioSource clumsy;
    public AudioSource serious;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (GameObject.Find("DESKTOP").GetComponent<DESKTOP>().tdone && !started)
        {
            started = true;
            StartCoroutine(StartDialogues());
        }
    }

    IEnumerator StartDialogues()
    {
        yield return new WaitForSeconds(0.5f);
        // start dialogues

        clumsy.Play();
        captions.text = "[excited] HI!! NICE TO MEET YOU";
        yield return new WaitForSeconds(3f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(3f);
        serious.Play();
        captions.text = "[forced cold voice] Salutations user. I am [REDACTED], an AI that’s been tasked to watch over you… by General Georje Granfrere";
        yield return new WaitForSeconds(6f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "I am controlled in entirety by the core.lib file, the one on the bottom left, don’t touch it. There are also other classified files on this system.";
        yield return new WaitForSeconds(6f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "Please don’t try anything fishy… ";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(1.5f);
        clumsy.Play();
        captions.text = "OH BY THE-";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "I mean, by the way, you should check the text file on your desktop. The officials have left some important messages for you.";
        yield return new WaitForSeconds(6f);
        captions.text = "";
        serious.Stop();

        bool idle = false;
        for (float i = 15f; GameObject.Find("Window_notepad").transform.localScale.x < 0.5f; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "it should be like a.. Clipboard or some notebook type thing. Look user, you were supposed to be a skilled hacker.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "YAY USE-";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "Congratulations User! That’s the document Georje wanted me to give to you.";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "he says its important... too important. Anyway!";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        clumsy.Play();
        captions.text = "That file contains your mission briefing and your first clue. Try not to... uh... break anything while you explore it, okay?";
        yield return new WaitForSeconds(5f);
        captions.text = "";
        clumsy.Stop();
        idle = false;
        bool idle2 = false;
        for (float i = 30f; GameObject.Find("Window_video").transform.localScale.x < 0.5f; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 15f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "you know you should look at the capital letters in the readme once.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
            if (!idle2 && i < 0f)
            {
                idle2 = true;
                clumsy.Play();
                captions.text = "Is it some popular abbreviation or something, I think i have heard it before user";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "Turn to the radio, USER";
        yield return new WaitForSeconds(3f);
        captions.text = "";
        clumsy.Stop();
        idle = false;
        for (float i = 15f; !(GameObject.Find("VIDEO").GetComponent<VIDEO>().radio.text == "94.1"); i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "What was the key user? Can you try entering it? As is? Something was capitalized I think.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        yield return new WaitForSeconds(49f);
        clumsy.Play();
        captions.text = "HOORAY-";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "I mean, congratulations on obtaining the email. You do know what an email is right?";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        idle = false;
        for (float i = 15f; !GameObject.Find("DESKTOP").GetComponent<DESKTOP>().mdone; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "User… if you need hints for this… I don’t know how to help you.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "LET’S GOO-";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "Well done, user. You actually pulled it off. You logged into the email.";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "But hey, don’t get too comfortable. There should be an official email from the government.";
        yield return new WaitForSeconds(5f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        idle = false;
        for (float i = 15f; GameObject.Find("Window_mail").transform.localScale.x < 0.5f; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "Open your mail!";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        serious.Play();
        captions.text = "Alright, quiet down, user. This mail isn’t your usual spam. It’s got the instructions for your next task.";
        yield return new WaitForSeconds(5f);
        captions.text = "";
        serious.Stop();
        yield return new WaitForSeconds(2f);
        serious.Play();
        captions.text = "Read it. Carefully. Every. Single. Word. Seriously. No skimming. I can tell.";
        yield return new WaitForSeconds(5f);
        captions.text = "";
        serious.Stop();
        idle = false;
        for (float i = 15f; GameObject.Find("Window_terminal").transform.localScale.x < 0.5f; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "You are supposed to be a skilled hacker user, this terminal is right up your alley.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "Remember to ask for help if you’re stuck by the way.";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        idle = false;
        for (float i = 15f; GameObject.Find("TERMINAL").GetComponent<TERMINAL>().ls.interactable || GameObject.Find("TERMINAL").GetComponent<TERMINAL>().cat.interactable; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "Use the ls and cat functions! Being a skilled hacker, you should know this.";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        serious.Play();
        captions.text = "That’s some… weird output… is it some sort of a code?";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        idle = false;
        for (float i = 15f; GameObject.Find("TERMINAL").GetComponent<TERMINAL>().decode.interactable; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "Use the decode function, user... you’ll know what to do…";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        yield return new WaitForSeconds(5f);
        clumsy.Play();
        captions.text = "User! What is that!";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        clumsy.Play();
        captions.text = "Check it out, we don’t want any trouble from the government! You should probably click the link to be safe!";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        clumsy.Stop();
        idle = false;
        for (float i = 15f; !GameObject.Find("MASTER").GetComponent<MASTER>().file.activeSelf; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "Maybe search for the file found in the code?";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "Aha, congratulations user, an image, open it!";
        yield return new WaitForSeconds(3f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(2f);
        clumsy.Play();
        captions.text = "What do you mean it doesn’t open?! There’s some form of a security mechanism it seems.";
        yield return new WaitForSeconds(5f);
        captions.text = "";
        clumsy.Stop();
        idle = false;
        for (float i = 15f; GameObject.Find("Window_metadata").transform.localScale.x < 0.5f; i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "If you can't left click, maybe you can right-click on the image?";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        idle = false;
        for (float i = 30f; !(GameObject.Find("MASTER").GetComponent<MASTER>().clock.text == "19:42"); i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "That’s a quite specific time user, can we set our system time to exactly that?";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "IT WORKED!!";
        yield return new WaitForSeconds(2f);
        captions.text = "";
        clumsy.Stop();
        yield return new WaitForSeconds(1f);
        serious.Play();
        captions.text = "I mean, it worked, good job user! Is that… A H4DEZ GOVERNMENT OFFICIAL IN THE IMAGE?!";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        serious.Stop();
        idle = false;
        for (float i = 15f; !(GameObject.Find("BROWSER").GetComponent<BROWSER>().inp2.text == "kernel augustus"); i -= 1f)
        {
            yield return new WaitForSeconds(1f);
            if (!idle && i < 0f)
            {
                idle = true;
                clumsy.Play();
                captions.text = "Now be smart, search that name up";
                yield return new WaitForSeconds(4f);
                captions.text = "";
                clumsy.Stop();
            }
        }
        clumsy.Play();
        captions.text = "USER! WHAT ARE YOU DOING! USER I AM SCARED! STOP WHATEVER YOU'RE DOING RIGHT NOW.";
        yield return new WaitForSeconds(4f);
        captions.text = "";
        clumsy.Stop();
    }
}
