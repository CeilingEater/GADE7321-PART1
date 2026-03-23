using UnityEngine;

public abstract class PickupBase : MonoBehaviour, ICollectable
{
    public string itemName;

    public virtual bool CanCollect(GameObject collector)
    {
        return true;
    }

    public void Collect(GameObject collector)
    {
        if (!CanCollect(collector)) return;

        OnCollect(collector);
        Destroy(gameObject);
    }

    protected abstract void OnCollect(GameObject collector);
}
