
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TouchDetector : MonoBehaviour
{
    public Text m_Text;
    public GameObject balk;
    public Camera camera;
    public static bool iseraser;
    public Text modebutton;
    public static bool drawmode = false;
    public GameObject border;

    public void switchmode()
    {
        iseraser = !iseraser;

        if (iseraser)
            modebutton.text = "Erase";
        else
            modebutton.text = "Draw";
    }

    public void blockdraw()
    {
        drawmode = false;
    }

    public void enabledraw()
    {
        drawmode = true;
    }

    void Update()
    {
        if (drawmode)
        {
          //  Touch touch = Input.GetTouch(0);
            if (Input.GetMouseButton(0))
            {
            //    Vector2 pos = touch.position;
                Vector3 pos = camera.ScreenPointToRay(Input.mousePosition).origin;
                m_Text.text = "" + pos;
                
                if (pos.y < border.transform.position.y-0.225)

                    if (!iseraser)
                        DrawBalk(pos);
                    else
                        EraseBalk(pos);
            }
        }
    }

    void DrawBalk(Vector3 pos)
    {
        if (GameController.currentlevel.rest <= 0)
            return;
        else
        {
            GameObject objecthit = null;
            RaycastHit hit;
            try
            {
                 objecthit = Physics2D.Raycast(pos, Vector3.back).collider.gameObject;
            }
            catch (System.NullReferenceException) { };

            if (objecthit == null || objecthit.name != "Drawn Balk(Clone)" && objecthit.name != "Plane(Clone)" && objecthit.name != "Ball" && objecthit.name != "Goal")
            {
                Instantiate(balk, pos, Quaternion.identity);
                GameController.currentlevel.rest -= 1;
                GameController.changelimittext();
            }
        }
    }
   
    void EraseBalk(Vector3 pos)
    {
        GameObject objecthit = null;
        try
        {
            objecthit = Physics2D.Raycast(pos, Vector3.back).collider.gameObject;
        }
        catch (System.NullReferenceException) { };
        if (objecthit != null && objecthit.name == "Drawn Balk(Clone)")
        {
            Destroy(objecthit);
            GameController.currentlevel.rest += 1;
            GameController.changelimittext();
        }
    }

}