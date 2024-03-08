using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContolCube : MonoBehaviour
{
    float speed = 300f;

    public Transform top;
    public GameObject moves;
    public GameObject main;
    public GameObject holder;
    public Transform up;
    public Transform left;
    public Transform front;
    public Transform right;
    public Transform back;
    public Transform bottom;

    public Button R;
    public Button F;
    public Button L;
    public Button B;
    public Button U;
    public Button D;
    public Button Ri;
    public Button Fi;
    public Button Li;
    public Button Bi;
    public Button Ui;
    public Button Di;
    public Button shuffle;
    public Button solve;

    Quaternion angle;
    Quaternion fix;
    string move;
    string move_set = "";
    List<int> ans = new List<int>();
    bool detect;
    bool listn;
    List<GameObject> map;
    List<int> mix = new List<int>();

    Vector2 PosStart;
    Vector2 PosEnd;
    Vector2 CurrentSwipe;
    Touch touch;
    Quaternion target;
    Quaternion orignal;

    void Start()
    {
        fix = top.transform.rotation;
        target = main.transform.rotation;
        orignal = target;
        detect = true;
        listn = true;
        map = new List<GameObject>();
        U.onClick.AddListener(rotateU);
        L.onClick.AddListener(rotateL);
        F.onClick.AddListener(rotateF);
        R.onClick.AddListener(rotateR);
        B.onClick.AddListener(rotateB);
        D.onClick.AddListener(rotateD);
        Ui.onClick.AddListener(rotateUi);
        Li.onClick.AddListener(rotateLi);
        Fi.onClick.AddListener(rotateFi);
        Ri.onClick.AddListener(rotateRi);
        Bi.onClick.AddListener(rotateBi);
        Di.onClick.AddListener(rotateDi);
        shuffle.onClick.AddListener(mixit);
        solve.onClick.AddListener(solve_move);
    }
    void Update()
    {
        listen();
        Swipe();
    }
    void listen()
    {
        if (listn == false)
        {
            float step = speed * Time.deltaTime;

            if (angle != top.transform.rotation)
            {
                top.transform.rotation = Quaternion.RotateTowards(top.transform.rotation, angle, step);
            }
            else if (top.transform.childCount != 0)
            {

                foreach (Transform child in top.transform)
                {
                    child.SetParent(holder.transform);
                }

            }
            else if (fix != top.transform.rotation)
            {
                top.transform.rotation = fix;
                angle = fix;
                move_text();

                for (int i = 1; i < 27; i++)
                {
                    Transform child = holder.transform.Find(string.Format($"Cube ({i})", i));
                    child.SetParent(main.transform);
                }

                for (int i = 1; i < 27; i++)
                {
                    Transform child = main.transform.Find(string.Format($"Cube ({i})", i));
                    child.SetParent(holder.transform);
                }
                updatemap(up, left, front, right, back, bottom);
                detector(true);
            }
        }
        else if (mix.Count > 0)
        {
            speed = 500;
            int r_move = mix[0];
            detect = false;
            if (r_move == 1)
            {
                rotateU();
            }
            else if (r_move == 2)
            {
                rotateL();
            }
            else if (r_move == 3)
            {
                rotateF();
            }
            else if (r_move == 4)
            {
                rotateR();
            }
            else if (r_move == 5)
            {
                rotateB();
            }
            else if (r_move == 6)
            {
                rotateD();
            }
            else if (r_move == 7)
            {
                rotateUi();
            }
            else if (r_move == 8)
            {
                rotateLi();
            }
            else if (r_move == 9)
            {
                rotateFi();
            }
            else if (r_move == 10)
            {
                rotateRi();
            }
            else if (r_move == 11)
            {
                rotateBi();
            }
            else if (r_move == 12)
            {
                rotateDi();
            }
            print(mix.Count);
            print(ans.Count);
            mix.RemoveAt(0);
        }
        else if (mix.Count == 0)
        {
            speed = 300f;
        }
    }

    void rotateU()
    {
        move = "U";
        detector(false);
        map = read();
        for (int i = 0; i < 9; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 90, 0);
    }
    void rotateB()
    {
        move = "B";
        detector(false);
        map =  read();
        for (int i = 9; i < 18; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 0, 90);
    }
    void rotateL()
    {
        move = "L";
        detector(false);
        map = read();
        for (int i =18; i < 27; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(90, 0, 0);
    }
    void rotateF()
    {
        move = "F";
        detector(false);
        map = read();
        for (int i = 27; i < 36; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 0, 90);
    }
    void rotateR()
    {
        move = "R";
        detector(false);
        map = read();
        for (int i = 36; i < 45; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(90, 0, 0);
    }
    void rotateD()
    {
        move = "D";
        detector(false);
        map = read();
        for (int i = 45; i < 54; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 90, 0);
    }
    void rotateUi()
    {
        move = "Ui";
        detector(false);
        map = read();
        for (int i = 0; i < 9; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, -90, 0);
    }
    void rotateBi()
    {
        move = "Bi";
        detector(false);
        map = read();
        for (int i = 9; i < 18; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 0, -90);
    }
    void rotateLi()
    {
        move = "Li";
        detector(false);
        map = read();
        for (int i = 18; i < 27; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(-90, 0, 0);
    }
    void rotateFi()
    {
        move = "Fi";
        detector(false);
        map = read();
        for (int i = 27; i < 36; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, 0, -90);
    }
    void rotateRi()
    {
        move = "Ri";
        detector(false);
        map = read();
        for (int i = 36; i < 45; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(-90, 0, 0);
    }
    void rotateDi()
    {
        move = "Di";
        detector(false);
        map = read();
        for (int i = 45; i < 54; i++)
        {
            Transform piece = map[i].transform.parent.transform;
            piece.SetParent(top);
        }
        angle = top.transform.rotation * Quaternion.Euler(0, -90, 0);
    }
    void Drag()
    {
        if (holder.transform.rotation != target)
        {
            var step = speed * Time.deltaTime;
            holder.transform.rotation = Quaternion.RotateTowards(holder.transform.rotation, target, step);
            detect = false;
        }
        else if (target != orignal)
        {
            target = orignal;
            for (int i = 1; i < 27; i++)
            {
                Transform child = holder.transform.Find(string.Format($"Cube ({i})", i));
                child.SetParent(main.transform);
            }

            holder.transform.rotation = orignal;

            for (int i = 1; i < 27; i++)
            {
                Transform child = main.transform.Find(string.Format($"Cube ({i})", i));
                child.SetParent(holder.transform);
            }
            updatemap(up, left, front, right, back, bottom);
            detector(true);
        }
    }
    void Swipe()
    {
        if (detect)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    PosStart = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    PosEnd = touch.position;
                    CurrentSwipe = new Vector2(PosEnd.x - PosStart.x, PosEnd.y - PosStart.y);
                    CurrentSwipe.Normalize();
                    if (LeftSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(0, 90, 0);
                    }
                    else if (RightSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(0, -90, 0);
                    }
                    else if (UpRightSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(-90, 0, 0);
                    }
                    else if (UpLeftSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(0, 0, 90);
                    }
                    else if (DownRightSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(90, 0, 0);
                    }
                    else if (DownLeftSwipe(CurrentSwipe))
                    {
                        U.interactable = false;
                        L.interactable = false;
                        F.interactable = false;
                        R.interactable = false;
                        B.interactable = false;
                        D.interactable = false;
                        Ui.interactable = false;
                        Li.interactable = false;
                        Fi.interactable = false;
                        Ri.interactable = false;
                        Bi.interactable = false;
                        Di.interactable = false;
                        shuffle.interactable = false;
                        solve.interactable = false;
                        target = holder.transform.rotation * Quaternion.Euler(0, 0, -90);
                    }
                    else if (NoSwipe(CurrentSwipe))
                    {
                        
                    }
                    detect = false;
                }
            }
        }
        else
        {
            Drag();
        }
    }
    public void mixit()
    {
        speed = 500f;
        for (int i = 0; i < 30; i++)
        {
            int r_move = Random.Range(1, 13);
            mix.Add(r_move);
        }
    }
    bool LeftSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x < 0 && CurrentSwipe.y > -0.5f && CurrentSwipe.y < 0.5f;
    }
    bool RightSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x > 0 && CurrentSwipe.y > -0.5f && CurrentSwipe.y < 0.5f;
    }
    bool UpRightSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x > 0f && CurrentSwipe.y < 0f;
    }
    bool UpLeftSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x < 0f && CurrentSwipe.y < 0f;
    }
    bool DownRightSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x < 0f && CurrentSwipe.y > 0f;
    }
    bool DownLeftSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x > 0f && CurrentSwipe.y > 0f;
    }
    bool NoSwipe(Vector2 swipe)
    {
        return CurrentSwipe.x == 0 && CurrentSwipe.y == 0;
    }
    public List<GameObject> read()
    {
        List<GameObject> colors = new List<GameObject>();

        RaycastHit hit;
        Vector3 top = new Vector3(0f, 2f, 0f);
        Vector3 left = new Vector3(-2f, 0f, 0f);
        Vector3 front = new Vector3(0f, -2f, 0f);
        Vector3 right = new Vector3(2f, 0f, 0f);
        Vector3 back = new Vector3(0f, 2f, 0f);
        Vector3 bottom = new Vector3(0f, -2f, 0f);

        for (int f = 0; f < 6; f++)
        {
            if (f == 0)
            {
                //top
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(x, 2f, y);
                        Vector3 direction = new Vector3(0f, -1f, 0f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (f == 4)
            {
                //back
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(2f, x, y);
                        Vector3 direction = new Vector3(-1f, 0f, 0f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (f == 3)
            {
                //right
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(x, y, -2f);
                        Vector3 direction = new Vector3(0f, 0f, 1f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (f == 2)
            {
                //front
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(-2f, y, x);
                        Vector3 direction = new Vector3(1f, 0f, 0f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (f == 1)
            {
                //left
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(x, y, 2f);
                        Vector3 direction = new Vector3(0f, 0f, -1f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (f == 5)
            {
                //bottom
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        Vector3 pos = new Vector3(x, -2f, y);
                        Vector3 direction = new Vector3(0f, 1f, 0f);
                        if (Physics.Raycast(pos, direction, out hit))
                        {
                            colors.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        return colors;
    }
    public void updatemap(Transform top, Transform left, Transform front, Transform right, Transform back, Transform bottom)
    {

        map = read();
        int i = 0;
        Color orange = new Color(1f, 0.5f, 0.2f);
        foreach (Transform panel in top)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
        foreach (Transform panel in left)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
        foreach (Transform panel in front)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
        foreach (Transform panel in right)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
        foreach (Transform panel in back)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
        foreach (Transform panel in bottom)
        {
            if (map[i].name == "tile_red")
            {
                panel.GetComponent<Image>().color = Color.red;
            }
            else if (map[i].name == "tile_blue")
            {
                panel.GetComponent<Image>().color = Color.blue;
            }
            else if (map[i].name == "tile_orange")
            {
                panel.GetComponent<Image>().color = orange;
            }
            else if (map[i].name == "tile_white")
            {
                panel.GetComponent<Image>().color = Color.white;
            }
            else if (map[i].name == "tile_yellow")
            {
                panel.GetComponent<Image>().color = Color.yellow;
            }
            else if (map[i].name == "tile_green")
            {
                panel.GetComponent<Image>().color = Color.green;
            }
            else
            {
                panel.GetComponent<Image>().color = Color.black;
            }
            i++;
        }
    }
    void move_text()
    {
        string anti_move = "";
        if (move.Length == 1)
        {
            anti_move = move + "i";
        }
        else if (move.Length == 2)
        {
            anti_move = move.Remove(1);
        }
        if (move_set != "")
        {
            if (move_set.Substring(move_set.Length - 1) == "i")
            {
                if (move_set.Substring(move_set.Length - 2) == anti_move)
                {
                    move_set = move_set.Remove(move_set.Length - 1);
                    move_set = move_set.Remove(move_set.Length - 1);
                    ans.RemoveAt(0);
                }
                else
                {
                    move_set += move;
                    solution();
                }
            }
            else
            {
                if (move_set.Substring(move_set.Length - 1) == anti_move)
                {
                    move_set = move_set.Remove(move_set.Length - 1);
                    ans.RemoveAt(0);
                }
                else
                {
                    move_set += move;
                    solution();
                }
            }
        }
        else
        {
            move_set += move;
            solution();
        }
        moves.GetComponent<Text>().text = move_set;
    }
    void solve_move()
    {
        foreach (int n in ans)
        {
            mix.Add(n);
        }
    }
    void solution()
    {
        if (move == "Ui")
        {
            ans.Insert(0, 1);
        }
        else if (move == "Li")
        {
            ans.Insert(0, 2);
        }
        else if (move == "Fi")
        {
            ans.Insert(0, 3);
        }
        else if (move == "Ri")
        {
            ans.Insert(0, 4);
        }
        else if (move == "Bi")
        {
            ans.Insert(0, 5);
        }
        else if (move == "Di")
        {
            ans.Insert(0, 6);
        }
        else if (move == "U")
        {
            ans.Insert(0, 7);
        }
        else if (move == "L")
        {
            ans.Insert(0, 8);
        }
        else if (move == "F")
        {
            ans.Insert(0, 9);
        }
        else if (move == "R")
        {
            ans.Insert(0, 10);
        }
        else if (move == "B")
        {
            ans.Insert(0, 11);
        }
        else if (move == "D")
        {
            ans.Insert(0, 12);
        }
    }
    void detector(bool dect)
    {
        U.interactable = dect;
        L.interactable = dect;
        F.interactable = dect;
        R.interactable = dect;
        B.interactable = dect;
        D.interactable = dect;
        Ui.interactable = dect;
        Li.interactable = dect;
        Fi.interactable = dect;
        Ri.interactable = dect;
        Bi.interactable = dect;
        Di.interactable = dect;
        shuffle.interactable = dect;
        solve.interactable = dect;
        detect = dect;
        listn = dect;
    }
}