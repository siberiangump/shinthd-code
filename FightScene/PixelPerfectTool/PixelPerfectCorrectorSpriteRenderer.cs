using Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCorrectorSpriteRenderer : MonoBehaviour
{
    [SerializeField] SpriteRenderer TargetSprite;
    [SerializeField] Camera RenderCamera;
    [SerializeField] float UpScale = 1;

    private void Update()
    {
        Correct();
    }

    [ContextMenu("Correct")] 
    public void Correct() 
    {
        Vector2 targetScale = new Vector2(TargetSprite.sprite.texture.width, TargetSprite.sprite.texture.height);
        Vector2 unitScale = (targetScale / TargetSprite.sprite.pixelsPerUnit);
        Vector3 center = this.transform.position;
        Vector3 scale = this.transform.lossyScale;
        Vector3 up = RenderCamera.WorldToScreenPoint((this.transform.rotation * new Vector3(0, (-unitScale.y/2)* scale.y, 0)) + center);
        Vector3 down = RenderCamera.WorldToScreenPoint((this.transform.rotation * new Vector3(0, (unitScale.y / 2) * scale.y, 0)) + center);
        Vector3 left = RenderCamera.WorldToScreenPoint((this.transform.rotation * new Vector3((- unitScale.x / 2) * scale.x, 0, 0)) + center);
        Vector3 right = RenderCamera.WorldToScreenPoint((this.transform.rotation * new Vector3((unitScale.x / 2) * scale.x, 0, 0)) + center);

        Vector2 screenScale = new Vector2(right.x - left.x, down.y - up.y);

        Vector2 scaleMod = (targetScale * UpScale) / screenScale;
        scaleMod.x = Mathf.Abs(scaleMod.x);
        
        this.transform.localScale *= scaleMod;
    }
}
