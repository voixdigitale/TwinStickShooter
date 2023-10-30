using UnityEngine;

public interface IHitable {
    void TakeHit(int teamId, GameObject hitSource);
}