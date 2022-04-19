using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] KeyCode Forward = KeyCode.UpArrow;
    [SerializeField] KeyCode Left = KeyCode.LeftArrow;
    [SerializeField] KeyCode Right = KeyCode.RightArrow;
    [SerializeField] KeyCode SwitchState = KeyCode.Space;

    [SerializeField] bool fightMode;

    private Tweener Tweener;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(SwitchState))
            DoSwitchState();
        if (fightMode)
            return;
        if (Tweener != null)
            return;
        if (Input.GetKeyUp(Forward))
        {
            Tweener = this.transform.DOLocalMove(this.transform.localPosition + (this.transform.localRotation*Vector3.right * 1.5f), .2f).OnComplete(ResetTweenerRef);
            return;
        }
        if (Input.GetKeyUp(Left))
        {
            Tweener = this.transform.DOLocalRotate(this.transform.rotation.eulerAngles + new Vector3(0, -90, 0), .2f).OnComplete(ResetTweenerRef);
            return;
        }
        if (Input.GetKeyUp(Right))
        {
            Tweener = this.transform.DOLocalRotate(this.transform.rotation.eulerAngles + new Vector3(0, 90, 0), .2f).OnComplete(ResetTweenerRef);
            return;
        }
    }

    private void DoSwitchState() 
    {
        fightMode = !fightMode;
        if (fightMode)
            animator.Play("CameraFightModeTransition");
        else
            animator.Play("CameraCharacterModeTransition"); 
    }

    private void ResetTweenerRef()
    {
        Tweener = null;
        //transform.GetChild(0).localPosition = Vector3.zero;
    }
}
