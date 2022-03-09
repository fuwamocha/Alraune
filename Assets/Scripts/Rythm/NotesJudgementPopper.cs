using UnityEngine;

public class NotesJudgementPopper : MonoBehaviour
{

    [SerializeField] private ComboCounter _combo;
    [SerializeField] private ScoreCounter _score;

    public void Failure(GameObject timing, int score)
    {
        //Debug.Log("Miss!");

        Instantiate(timing, this.transform.position, Quaternion.identity);

        _score.AddScore(score);
        _combo.ResetCombo();
    }

    public void Success(GameObject timing, int score)
    {
        Instantiate(timing, this.transform.position, Quaternion.identity);

        _score.AddScore(score);
        _combo.AddCombo(1);
    }
}
