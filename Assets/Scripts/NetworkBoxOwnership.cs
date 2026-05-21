using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkBoxOwnership : MonoBehaviourPun, IPunOwnershipCallbacks
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void OnMouseDown()
    {
        if (!photonView.IsMine)
        {
            Debug.Log("Requesting ownership of box...");
            photonView.RequestOwnership();
        }
        else
        {
            Debug.Log("You already own this box.");
        }
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != photonView)
            return;

        Debug.Log($"Ownership requested by ActorNumber: {requestingPlayer.ActorNumber}");

        photonView.TransferOwnership(requestingPlayer);
    }

    // PERHATIKAN NAMA METHOD: Transfered
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView != photonView)
            return;

        string previous = previousOwner != null ? previousOwner.ActorNumber.ToString() : "none";
        Debug.Log($"Ownership transferred. Previous owner: {previous}, New owner: {photonView.OwnerActorNr}");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        if (targetView != photonView)
            return;

        Debug.LogWarning("Ownership transfer failed for player: " + senderOfFailedRequest.ActorNumber);
    }
}