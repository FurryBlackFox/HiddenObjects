using UnityEngine;

namespace Ingame
{
    public class ClickableObjectInitParams
    {
        public Vector2 targetSize;
        public Sprite Sprite;
        public Vector2 position;

        public ClickableObjectInitParams(Vector2 targetSize, Sprite sprite, Vector2 position)
        {
            this.targetSize = targetSize;
            Sprite = sprite;
            this.position = position;
        }
    }
}