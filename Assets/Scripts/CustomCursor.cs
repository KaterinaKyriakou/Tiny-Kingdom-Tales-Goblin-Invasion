using UnityEngine;

public class CustomCursor : MonoBehaviour
{
   public Texture2D cursorTexture;
   void Start()
   {
     SetCustomCursor();
   }

   private void SetCustomCursor()
   {
    Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
   }
}
