using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Settings")]
    public float swayClamp_ = 0.1f;
    public float smoothing_ = 1f;

    private Vector3 origin;
    private FPSPlayerStats _playerStats;

    private void Start()
    {
        origin = transform.localPosition;
        _playerStats = GetComponentInParent<FPSPlayerStats>();
    }

    private void FixedUpdate()
    {
        Vector2 input = _playerStats.useOldInput_ ? new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))
                         : _playerStats._PlayerInputNew._CameraInput;

        input.x = Mathf.Clamp(input.x, -swayClamp_, swayClamp_);
        input.y = Mathf.Clamp(input.y, -swayClamp_, swayClamp_);

        Vector3 target = new Vector3(-input.x, -input.y, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.deltaTime * smoothing_);
    }
}
