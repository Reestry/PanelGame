using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: Header("Camera")]
    [field: Tooltip("Амплитуда покачивания камеры при ходьбе")]
    [field: SerializeField] public float WalkAmplitude { get; private set; } = 1.5f;

    [field: Tooltip("Амплитуда покачивания камеры при спринте")]
    [field: SerializeField] public float SprintAmplitude { get; private set; } = 3.5f;

    [field: Tooltip("Частота покачивания камеры при ходьбе")]
    [field: SerializeField] public float WalkFrequency { get; private set; } = 2f;

    [field: Tooltip("Частота покачивания камеры при спринте")]
    [field: SerializeField] public float SprintFrequency { get; private set; } = 4f;

    
    [field: Header("Movement")] 
    [field: Tooltip("Скорость обычной ходьбы")]
    [field: SerializeField] public float WalkSpeed { get; private set; } = 5f;

    [field: Tooltip("Скорость бега (спринта)")]
    [field: SerializeField] public float SprintSpeed { get; private set; } = 10f;

    [field: Tooltip("Скорость перемещения в приседе")]
    [field: SerializeField] public float CrouchSpeed { get; private set; } = 2f;

    [field: Tooltip("Контроль персонажа в воздухе (0 — неуправляемый, 1 — полный контроль)")]
    [field: SerializeField] public float AirControl { get; private set; } = 0.3f;

    
    [field: Header("Jump & Gravity")] 
    [field: Tooltip("Сила прыжка персонажа")]
    [field: SerializeField] public float JumpForce { get; private set; } = 3f;

    [field: Tooltip("Гравитация, применяемая к персонажу")]
    [field: SerializeField] public float Gravity { get; private set; } = -9.81f;

    [field: Tooltip("Сила, с которой персонаж толкает физические объекты")]
    [field: SerializeField] public float ItemPushForce { get; private set; } = 10f;

    
    [field: Header("Interact")] 
    [field: Tooltip("Максимальная дистанция взаимодействия с объектами")]
    [field: SerializeField] public float MaxLength { get; private set; } = 5f;
    
    [field: Tooltip("Сила перемещения объектов")]
    [field: SerializeField] public float TakeForce { get; private set; } = 20f;

    
    [field: Header("Mouse sens")] 
    [field: Tooltip("Чувствительность мыши")]
    [field: SerializeField] public float MouseSens { get; private set; } = 5f; 
}
