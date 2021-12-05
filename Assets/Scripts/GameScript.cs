using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject circlePrefab;
    public GameObject scoreBoard;
    public Transform canvas;

    GameObject board;
    GameObject newLine;
    GameObject line2Destroy;
    GameObject circle;
    Circle circleScript;
    
    Color randomColor;
    int highscroe = 0;
    public Text scoreTxt;
    int score = 0;

    Vector3 pos1;
    Vector3 pos2;
    bool create_vertical;

    private void Start()
    {
        highscroe = PlayerPrefs.GetInt("highScore");
        GameStart();
    }
    void OnMouseDown()
    {

        if (circle == null)
        {
            score = 0;
            Destroy(board);
            Destroy(newLine);
            Destroy(line2Destroy);
            GameStart();
            return;
        }
        Vector2 inters = FindIntersection(newLine.GetComponent<LineRenderer>(), line2Destroy.GetComponent<LineRenderer>());
        if (!circleScript.onPoint(inters))
        {
            if (score > highscroe)
                highscroe = score;
            PlayerPrefs.SetInt("highScore",highscroe);
            Destroy(circle);
            board = Instantiate(scoreBoard);
            board.GetComponent<ScoreScript>().WriteScore(score, highscroe);
            return;
        }
        score += 1;
        circleScript.score = score;
        circle.transform.position = inters;
        circle.GetComponent<SpriteRenderer>().color = randomColor;
        scoreTxt.text = score.ToString();
        Destroy(line2Destroy);
        line2Destroy = newLine;
        circleScript.movDir = pos1 - pos2;
        if (create_vertical)
        {
            pos1 = new Vector3(Random.Range(10, 710), 0);
            pos2 = new Vector3(Random.Range(10, 710), 1280);
        }
        else
        {
            pos1 = new Vector3(0, Random.Range(10, 1270));
            pos2 = new Vector3(720, Random.Range(10, 1270));
        }
        newLine = CreateLine(pos1, pos2);
        randomColor = Random.ColorHSV();
        newLine.GetComponent<LineRenderer>().SetColors(randomColor, randomColor);
        create_vertical = !create_vertical;


    }

    public void GameStart()
    {
        create_vertical = false;
        scoreTxt.text = "Tap to start";
        pos1 = new Vector3(Random.Range(10, 710), 0);
        pos2 = new Vector3(Random.Range(10, 710), 1280);
        newLine = CreateLine(pos1, pos2);
        line2Destroy = CreateLine(new Vector3(0, 640), new Vector3(720, 640));
        randomColor = Random.ColorHSV();
        newLine.GetComponent<LineRenderer>().SetColors(randomColor, randomColor); 

        circle = Instantiate(circlePrefab, new Vector3(50, 640), Quaternion.identity, canvas);
        circleScript = circle.GetComponent<Circle>();
        circleScript.movDir = new Vector3(1, 0);
    }

    GameObject CreateLine(Vector3 pos1, Vector3 pos2)
    {
        LineRenderer lr;
        GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, canvas);
        lr = line.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, pos1);
        lr.SetPosition(1, pos2);
        return line;
    }

    Vector2 FindIntersection(LineRenderer lr1, LineRenderer lr2)
    {
        Vector2 lr1Point1 = lr1.GetPosition(0);
        Vector2 lr1Point2 = lr1.GetPosition(1);
        Vector2 lr2Point1 = lr2.GetPosition(0);
        Vector2 lr2Point2 = lr2.GetPosition(1);

        float m1 = (lr1Point1.y - lr1Point2.y) / (lr1Point1.x - lr1Point2.x);
        float m2 = (lr2Point1.y - lr2Point2.y) / (lr2Point1.x - lr2Point2.x);
        float y = (m2 * (lr1Point1.x - lr2Point1.x) + lr2Point1.y - m2 / m1 * lr1Point1.y) / (1 - m2 / m1);
        float x = lr1Point1.x + (y - lr1Point1.y) / m1;

        return new Vector2(x, y);
    }
}
