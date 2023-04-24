using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpenClose : MonoBehaviour
{
	[Header("Script references")]
	[SerializeField] private ThirdPersonController tpController;
	[SerializeField] private ThirdPersonShooter tpShooter;
	[SerializeField] private StarterAssetsInputs _input;

	private GameObject inventoryPanel;

	private bool inventoryOpen;

	private void Start()
	{
		inventoryPanel = ReferenceManager.instance.inventoryPanel;
		inventoryOpen = false;
	}

	private void Update()
	{
		InventoryTriggerCheck();
	}

	private void InventoryTriggerCheck()
	{
		if(_input._InvetoryOpenClose.WasPressedThisFrame() && !inventoryOpen)
		{
			OpenInventory();
		}
		else if(_input._InvetoryOpenClose.WasPressedThisFrame() && inventoryOpen)
		{
			CloseInventory();
		}
	}

	private void OpenInventory()
	{
		inventoryPanel.gameObject.SetActive(true);
		inventoryOpen = true;

		tpController.enabled = false; 
		tpShooter.enabled = false;
	}

	private void CloseInventory()
	{
		inventoryPanel.gameObject.SetActive(false);
		inventoryOpen = false;

		tpController.enabled = true;
		tpShooter.enabled = true;
	}

}
