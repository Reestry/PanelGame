using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig" , menuName = "Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    [Header("Camera")]
    [SerializeField] private float _walkAmplitude = 1.5f;
    [SerializeField] private float _sprintAmplitude = 3.5f;
    [SerializeField] private float _walkFrequency = 2; //
    [SerializeField] private float _sprintFrequency = 4f; //
    
    [Header("Movement")] 
    [SerializeField] private float _walkSpeed = 5f; //
    [SerializeField] private float _sprintSpeed = 10f; //
    [SerializeField] private float _crouchSpeed = 2f; //
    [SerializeField] private float _airControl = 0.3f; //
    
    [Header("Jump & Gravity")] 
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _itemPushForce = 10f;
    
    
    [Header("Interact")] 
    [SerializeField] private float _maxLenghth = 5f; //
    
    [Header("Mouse sens")] 
    [SerializeField] private float _mouseSens = 5; //
    
}