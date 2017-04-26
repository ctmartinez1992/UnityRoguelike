using UnityEngine;
using System.Collections;
using System;

public enum DOOR_STATE {
    UNKNOWN = 0,
    OPENED = 1,
    CLOSED = 2,
    LOCKED = 3
}

public class Door : InteractibleComponent {

    public AudioClip opening1Audio;
    public AudioClip opening2Audio;
    public AudioClip closing1Audio;
    public AudioClip closing2Audio;
    public AudioClip lockingAudio;
    public AudioClip unlockingAudio;
    public AudioClip lockedAudio;

    public Sprite openedSprite;
    public Sprite closedSprite;
    public Sprite lockedSprite;
    public Sprite secretSprite;

    public int matchingKeyID;

    public DOOR_STATE state;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box2D;

    protected void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        box2D = gameObject.GetComponent<BoxCollider2D>();
    }

    public override void Use() {
        if(state == DOOR_STATE.CLOSED || state == DOOR_STATE.LOCKED) {
            Open();
        }
        else if(state == DOOR_STATE.OPENED) {
            Close();
        }
    }

    public override void Interact() {
        Debug.Log("It's a door, a good place to put the check for traps check.");
    }

    public override void WalkInto() {
        Open();
    }

    public void LinkToKey(int keyID) {
        this.matchingKeyID = keyID;
    }

    //DamageWall is called when the player attacks a wall.
    public void Open() {
        if(state == DOOR_STATE.LOCKED) {
            SoundManager.instance.PlaySingle(lockedAudio);
            Debug.Log("Door is locked.");
        }
        else if(state == DOOR_STATE.CLOSED) {
            SoundManager.instance.RandomizeSfx(opening1Audio, opening2Audio);
            spriteRenderer.sprite = openedSprite;
            state = DOOR_STATE.OPENED;
            box2D.isTrigger = true;
            Debug.Log("Door was opened.");
        }
        else {
            Debug.Log("Door is already opened.");
        }
    }

    public void Close() {
        if(state == DOOR_STATE.OPENED) {
            SoundManager.instance.RandomizeSfx(closing1Audio, closing2Audio);
            spriteRenderer.sprite = closedSprite;
            state = DOOR_STATE.CLOSED;
            box2D.isTrigger = false;
            Debug.Log("Door was closed.");
        }
        else if(state == DOOR_STATE.LOCKED) {
            Debug.Log("Door is locked.");
        }
        else if(state == DOOR_STATE.CLOSED) {
            Debug.Log("Door is already closed.");
        }
    }

    public void Lock(int keyID) {
        if(state == DOOR_STATE.CLOSED) {
            if(keyID == matchingKeyID) {
                SoundManager.instance.PlaySingle(lockingAudio);
                spriteRenderer.sprite = lockedSprite;
                state = DOOR_STATE.LOCKED;
                box2D.isTrigger = false;
                Debug.Log("Door was locked.");
            }
        }
    }

    public void Unlock(int keyID) {
        if(keyID == matchingKeyID) {
            SoundManager.instance.PlaySingle(unlockingAudio);
            spriteRenderer.sprite = closedSprite;
            state = DOOR_STATE.CLOSED;
        }
    }
}