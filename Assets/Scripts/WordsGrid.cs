using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsGrid : MonoBehaviour
{
    public GameData CurrentGameData;
    public GameObject gridSquarePrefab;
    public AlphabetData alphabetData;

    public float squareoffset = 0.0f;
    public float topPosition;
    private List<GameObject>_squareList=new List<GameObject>();

    void Start()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }
    private void SetSquaresPosition()
    {
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();
        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareoffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareoffset) * 0.01f
        };

        var startPosition = GetFirstSquarePosition();
        int columnNUmber = 0;
        int rowNumber = 0;

        foreach (var square in _squareList)
        {
            if (rowNumber+1>CurrentGameData.selectedboardData.Rows)
            {
                columnNUmber++;
                rowNumber = 0;
            }
            var positionX = startPosition.x + offset.x * columnNUmber;
            var positionY = startPosition.y + offset.y * rowNumber;
            square.GetComponent<Transform>().position=new Vector2(positionX, positionY);
            rowNumber++;
        }
    }
    private Vector2 GetFirstSquarePosition() 
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x=squareRect.width*squareTransform.localScale.x;
        squareSize.y=squareRect.height*squareTransform.localScale.y;

        var midWidthPosition = (((CurrentGameData.selectedboardData.Columns - 1) * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = (((CurrentGameData.selectedboardData.Rows - 1) * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1: midWidthPosition;
        startPosition.y += midWidthHeight;
        return startPosition;
    }
    private void SpawnGridSquares()
    {
        if (CurrentGameData!=null)
        {
            var squareScale = GetSquareScale(new Vector3(1.5f, 1.5f, 0.1f));
            foreach (var squares in CurrentGameData.selectedboardData.Board)
            {
                foreach (var squareLetter in squares.Row)
                {
                    var normalLetterData = alphabetData.AlphabetNormal.Find(data => data.letter == squareLetter);
                    var selectedLetterData=alphabetData.AlphabetHighlighted.Find(data => data.letter == squareLetter);
                    var corectLetterData=alphabetData.AlphabetWrong.Find(data => data.letter == squareLetter);
                    if (normalLetterData.image==null||selectedLetterData.image==null)
                    {
                        Debug.LogError("All fields in ur array should have some letters .Press fill up with random btn in ur board data to add random letter.letter:" + squareLetter);

                        #if UNITY_EDITOR
                        if (UnityEditor.EditorApplication.isPlaying)
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
                        #endif
                    }
                    else
                    {
                        _squareList.Add(Instantiate(gridSquarePrefab));
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetsSprite(normalLetterData, corectLetterData, selectedLetterData);
                        _squareList[_squareList.Count - 1].transform.SetParent(this.transform);
                        _squareList[_squareList.Count - 1].GetComponent<Transform>().position=new Vector3(0f, 0f, 0f);
                        _squareList[_squareList.Count - 1].transform.localScale = squareScale;
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetIndex(_squareList.Count-1);
                    }
                }   
            }
        }
    }
    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finaleScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finaleScale))
        {
            finaleScale.x -= adjustment;
            finaleScale.y-=adjustment;

            if (finaleScale.x<=0||finaleScale.y<=0)
            {
                finaleScale.x = adjustment;
                finaleScale.y= adjustment;
                return finaleScale;
            }
        }
        return finaleScale;
    }
    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f);

        squareSize.x=(squareRect.width*targetScale.x)+squareoffset;
        squareSize.y=(squareRect.height*targetScale.y)+squareoffset;

        var midWidthPosition = ((CurrentGameData.selectedboardData.Columns * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = ((CurrentGameData.selectedboardData.Rows * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y = midWidthHeight;

        return (startPosition.x < GetHalfScreenWidth() * -1 || startPosition.y > topPosition);
    }
    private float GetHalfScreenWidth()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = (1.7f * height) * Screen.width / Screen.height;
        return width / 2;
    }
}
