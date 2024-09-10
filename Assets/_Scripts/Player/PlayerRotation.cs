using UnityEngine;

public class PlayerRotation : MonoBehaviour
{


    private CharacterController m_characterController;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

    }


}
