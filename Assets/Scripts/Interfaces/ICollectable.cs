using UnityEngine;

public interface ICollectable 
{
    void Collect(GameObject collector);
    bool CanCollect(GameObject collector);
}
