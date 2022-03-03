using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerater : MonoBehaviour
{
    private int notesInterval = 2;
    private int notes = 0;

    /// <summary>
    /// MIDIデータ上で音が鳴ったらnotesIntervalの間隔を空けてノーツを生成するクラス
    /// </summary>
    public void GenerateNotes()
    {
        notes++;
        if (notes%notesInterval==0)
        Debug.Log("aaaaa");
    }
}
