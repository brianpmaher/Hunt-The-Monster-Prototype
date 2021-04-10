using HuntTheMonster.EventChannels;
using UnityEngine;

namespace HuntTheMonster.UI
{
    public class MessagesContainer : MonoBehaviour
    {
        [SerializeField] private GameObject feelDraftText;
        [SerializeField] private BoolEventChannel feelDraftChannel;
        [SerializeField] private GameObject smellOdorText;
        [SerializeField] private BoolEventChannel smellOdorChannel;

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