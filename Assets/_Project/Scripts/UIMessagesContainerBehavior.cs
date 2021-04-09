using HuntTheMonster.EventChannels;
using UnityEngine;

namespace HuntTheMonster
{
    public class UIMessagesContainerBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject feelDraftText;
        [SerializeField] private BoolEventChannelSO feelDraftChannel;
        [SerializeField] private GameObject smellOdorText;
        [SerializeField] private BoolEventChannelSO smellOdorChannel;

        private void OnEnable()
        {
            feelDraftChannel.OnEventRaised += HandleFeelDraft;
            smellOdorChannel.OnEventRaised += HandleSmellOdor;
        }

        private void OnDisable()
        {
            feelDraftChannel.OnEventRaised -= HandleFeelDraft;
            smellOdorChannel.OnEventRaised -= HandleSmellOdor;
        }

        private void HandleFeelDraft(bool feelsDraft)
        {
            feelDraftText.SetActive(feelsDraft);
        }

        private void HandleSmellOdor(bool smellsOdor)
        {
            smellOdorText.SetActive(smellsOdor);
        }
    }
}