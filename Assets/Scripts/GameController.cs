using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameObject startingmenu;
    public static GameObject topmenu;
    public static GameObject leveldonemenu;
    public static Text limittext;

    public GameObject immutablebalk;
    public GameObject ball;
    public GameObject goal;

    public static List<Level> levels = new List<Level>();
    public static Level currentlevel;
    static int currentlevelnum;

    // Start is called before the first frame update
    void Start()
    {
        Saver.Load();
        if (levels.Count == 0)
            generatelevels();

        for (int i = 0; i < levels.Count; i++)
            if (levels[i].done)
                GameObject.Find($"StartingMenu/{i + 1}/Done").GetComponent<Toggle>().isOn = true;


        startingmenu = GameObject.Find("StartingMenu");
        topmenu = GameObject.Find("TopMenu");
        leveldonemenu = GameObject.Find("LevelDoneMenu");
        limittext = GameObject.Find("Limit").GetComponent<Text>();
        topmenu.active = false;
        leveldonemenu.active = false;
    }

    void generatelevels()
    {
        List<Balk> l = new List<Balk>();
        l.Add(new Balk(new Vector2(-2, 0), Quaternion.Euler(0, 0, -13), new Vector3(2, 0.2f, 1)));
        Level lev = new Level(l, new Vector3(-2, 4, -2), new Vector3(0, -0.88f, -2), 100);
        levels.Add(lev);

        l = new List<Balk>();
        l.Add(new Balk(new Vector2(1, -3), Quaternion.Euler(0, 0, 0), new Vector3(4, 0.2f, 1)));
        lev = new Level(l, new Vector3(-2, 4, -2), new Vector3(2, -4, -2), 100);
        levels.Add(lev);

        l = new List<Balk>();
        l.Add(new Balk(new Vector2(0, 0), Quaternion.Euler(0, 0, 0), new Vector3(4, 0.2f, 1)));
        lev = new Level(l, new Vector3(0, 4, -2), new Vector3(0, -2, -2), 100);
        levels.Add(lev);

        l = new List<Balk>();
        l.Add(new Balk(new Vector2(0, 0), Quaternion.Euler(0, 0, 45), new Vector3(5, 0.2f, 1)));
        l.Add(new Balk(new Vector2(0, 0), Quaternion.Euler(0, 0, -45), new Vector3(5, 0.2f, 1)));
        lev = new Level(l, new Vector3(0, 4, -2), new Vector3(0, -2, -2), 100);
        levels.Add(lev);

        l = new List<Balk>();
        l.Add(new Balk(new Vector2(0, 1), Quaternion.Euler(0, 0, 0), new Vector3(1.5f, 0.2f, 1)));
        l.Add(new Balk(new Vector2(0.65f, 0.3f), Quaternion.Euler(0, 0, 90), new Vector3(1.5f, 0.2f, 1)));
        l.Add(new Balk(new Vector2(-0.65f, 0.3f), Quaternion.Euler(0, 0, 90), new Vector3(1.5f, 0.2f, 1)));
        lev = new Level(l, new Vector3(0, 4, -2), new Vector3(0, 0, -2), 100);
        levels.Add(lev);
    }

    public static void windowswitch()
    {
        startingmenu.active = !startingmenu.active;
        topmenu.active = !topmenu.active;
    }

    public void startlevel(int num)
    {
        currentlevelnum = num;
        currentlevel = levels[currentlevelnum - 1];
        createbalks(currentlevel.balks);
        currentlevel.rest = currentlevel.limit;
        changelimittext();
        setballs();

        windowswitch();
        TouchDetector.drawmode = true;
    }

    public void setballs()
    {
        ball.transform.localPosition = currentlevel.start;
        ball.GetComponent<Rigidbody2D>().simulated = false;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        goal.transform.localPosition = currentlevel.end;
    }

    public void playlevel()
    {
        ball.GetComponent<Rigidbody2D>().simulated = true;
    }

    public static void completelevel()
    {
        currentlevel.done = true;
        leveldonemenu.active = true;
    }

    public void backtomenu()
    {
        ball.GetComponent<Rigidbody2D>().simulated = false;
        ball.transform.localPosition = new Vector3(0, 4, 2);
        goal.transform.localPosition = new Vector3(0, 8, 2);
        leveldonemenu.active = false;
        clearspace();
        TouchDetector.drawmode = false;
        windowswitch();
        if (currentlevel.done)
            GameObject.Find($"StartingMenu/{currentlevelnum}/Done").GetComponent<Toggle>().isOn = true;
        currentlevelnum = -1;

    }

    public void reset()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].done = false;
            GameObject.Find($"StartingMenu/{i + 1}/Done").GetComponent<Toggle>().isOn = false;
        }
    }

    public static void GetFromSave(List<Level> _levels)
    {
        levels = _levels;
    }

    public void createbalks(List<Balk> balks)
    {
        foreach (Balk balk in balks)
        {
            GameObject newbalk = Instantiate(immutablebalk, new Vector3(balk.position.x, balk.position.y, 90), balk.rotation);
            newbalk.transform.localScale = balk.scale;
        }
    }

    public static void clearspace()
    {
        GameObject[] todestroy = GameObject.FindGameObjectsWithTag("Todestroy");
        for (int i = 0; i < todestroy.Length; i++)
        {
            Destroy(todestroy[i]);
        }
    }

    public static void changelimittext()
    {
        limittext.text = $"Balk length limit: {currentlevel.limit} Left: {currentlevel.rest}";
    }

}
