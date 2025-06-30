using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
   Animator animator; 
   int horizontal;
   int vertical;

   private void Awake()
   {
      animator = GetComponent<Animator>();
      horizontal = Animator.StringToHash("Horizontal");
      vertical = Animator.StringToHash("Vertical");
   }

   public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
   {
      float snappendHorizontal;
      float snappendVertical;
      
      #region Snapped Horizontal
      if (horizontalMovement > 0 && horizontalMovement < 0.55f)
      {
         snappendHorizontal = 0.5f;
      }
      else if (horizontalMovement > 0.55f)
      {
         snappendHorizontal = 1;
      }
      else if (horizontalMovement == 0 && horizontalMovement < -0.55f)
      {
         snappendHorizontal = -0.5f;
      }
      else if (horizontalMovement < -0.55f)
      {
         snappendHorizontal = -1;
      }
      else
      {
         snappendHorizontal = 0;
      }
      #endregion
      #region Snapped Vertical
      if (verticalMovement > 0 && verticalMovement < 0.55f)
      {
         snappendVertical = 0.5f;
      }
      else if (verticalMovement > 0.55f)
      {
         snappendVertical = 1;
      }
      else if (verticalMovement == 0 && verticalMovement < -0.55f)
      {
         snappendVertical = -0.5f;
      }
      else if (verticalMovement < -0.55f)
      {
         snappendVertical = -1;
      }
      else
      {
         snappendVertical = 0;
      }
      #endregion

      if (isSprinting)
      {
         snappendHorizontal = horizontalMovement;
         snappendVertical = 2;
      }
      
      animator.SetFloat(horizontal, snappendHorizontal, 0.1f, Time.deltaTime);
      animator.SetFloat(vertical, snappendVertical, 0.1f, Time.deltaTime);
   }
}
