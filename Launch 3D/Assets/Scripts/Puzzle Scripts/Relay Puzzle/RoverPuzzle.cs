using UnityEngine;
using System.Collections;

public class RoverPuzzle : MonoBehaviour {

	public GameObject dialogue;
	public GameObject nextLog;
	//Curiosity
	public GameObject CuriosityRover;
	public GameObject CuriosityGround;
	public bool CuriosityAlive = true;
	public int CuriosityTree = 1;
	//Spirit
	public GameObject SpiritRover;
	public GameObject SpiritGround;
	public bool SpiritAlive = false;
	public int SpiritTree = 5;
	//Opportunity
	public GameObject OpportunityRover;
	public GameObject OpportunityGround;
	public bool OpportunityAlive = false;
	public int OpportunityTree = 5;
	//Sojourner
	public GameObject SojournerRover;
	public GameObject SojournerGround;
	public bool SojournerAlive = false;
	public int SojournerTree = 5;
	//FifthRover
	public GameObject FifthRover;
	public GameObject FifthGround;
	public bool FifthAlive = false;
	public int FifthTree = 5;
	//Selected
	public GameObject SelectedRover;
	public GameObject SelectedGround;
	public bool SelectedAlive = false;
	public int SelectedTree = 7;
	//TempSelected
	private GameObject TempRover;
	private GameObject TempGround;
	public bool TempAlive = false;
	public int TempTree = 7;
	public GameObject InvalidGround;
	public GameObject FinishObj;

	RaycastHit hit;
	public Camera minigameCamera;
	public AudioClip incorrectSound;
	public AudioClip winSound;
	public bool YouWon = false;
	public Material RoverColor;
	public Material RoverSelected;
	public Material RoverAlive;
	public Material FinWin;

	public GameObject FinObj;

	public Transform player;
	public GameObject mainCamera;
	public GameObject miniCam;
	public GameObject instructions;
	private FirstPersonCharacter FPCscript;
	public GameObject RoverPuzzleObject;
	private RoverPuzzle RoverPuzzleScript;

	public GameObject CuriosityPing;
	public GameObject SpiritPing;
	public GameObject OpportunityPing;
	public GameObject SojournerPing;
	public GameObject FifthPing;
	public GameObject FinalPing;

	public string journalEntry;
	public DoorScript finalDoor;
	public GameObject reticle;
	
	void Start() {
		PlayerPrefs.DeleteAll ();

		FPCscript = player.GetComponent<FirstPersonCharacter> ();
		PersistantGlobalScript.interactionEnabled = false;
		PersistantGlobalScript.mouseLookEnabled = false;
		PersistantGlobalScript.movementEnabled = false; 
		FPCscript.lockCursor = false;
		Screen.lockCursor = false;

	//	FPCscript = player.GetComponent<FirstPersonCharacter> ();
		RoverPuzzleScript = RoverPuzzleObject.GetComponent<RoverPuzzle> ();


		hit = new RaycastHit();
		//print ("i am aliiiiiive");

		//Load saved rover positions and puzzle completion
		CuriosityRover.transform.position = PlayerPrefsX.GetVector3("CuriosityRover", CuriosityRover.transform.position);
		SpiritRover.transform.position = PlayerPrefsX.GetVector3("SpiritRover", SpiritRover.transform.position);
		OpportunityRover.transform.position = PlayerPrefsX.GetVector3("OpportunityRover", OpportunityRover.transform.position);
		SojournerRover.transform.position = PlayerPrefsX.GetVector3("SojournerRover", SojournerRover.transform.position);
		FifthRover.transform.position = PlayerPrefsX.GetVector3("FifthRover", FifthRover.transform.position);
		YouWon = PlayerPrefsX.GetBool("RoverWin", false);
	}
	
	void Update () {
		Alive ();
		Selecting ();
	}

	public void Exit() {
		print ("Do the thing.");
		
		PersistantGlobalScript.interactionEnabled = true;
		PersistantGlobalScript.mouseLookEnabled = true;
		PersistantGlobalScript.movementEnabled = true;
		player.gameObject.SetActive(true);
		instructions.SetActive(false);
		CuriosityPing.SetActive(false);
		OpportunityPing.SetActive(false);
		SpiritPing.SetActive(false);
		SojournerPing.SetActive(false);
		FifthPing.SetActive(false);
		FinalPing.SetActive (false);
		
		reticle.SetActive(true);
		FPCscript.lockCursor = true;
		PersistantGlobalScript.minigameActive = false;
		RoverPuzzleObject.collider.enabled=true;
		mainCamera.SetActive(true);
		miniCam.SetActive(false);
		Screen.lockCursor = true;
		RoverPuzzleScript.enabled=false;
	}
	
	void Selecting() {
	//void OnMouseUp() {
		Ray ray = minigameCamera.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonUp(0)) {
			if (Physics.Raycast (ray, out hit, 100)) { 
				//set the temporary selected rover equal to gameobject in hit
				TempRover = hit.collider.gameObject;
				//print ("hey look i did something is this really working " + TempRover.name);

				//check the temporary object against all rovers
				if (TempRover == CuriosityRover) {
					TempGround = CuriosityGround;
					TempAlive = CuriosityAlive;
					TempTree = CuriosityTree;
				} 
				else if (TempRover == SpiritRover) {
					TempGround = SpiritGround;
					TempAlive = SpiritAlive;
					TempTree = SpiritTree;
				} 
				else if (TempRover == OpportunityRover) {
					TempGround = OpportunityGround;
					TempAlive = OpportunityAlive;
					TempTree = OpportunityTree;
				} 
				else if (TempRover == SojournerRover) {
					TempGround = SojournerGround;
					TempAlive = SojournerAlive;
					TempTree = SojournerTree;
				}
				else if (TempRover == FifthRover) {
					TempGround = FifthGround;
					TempAlive = FifthAlive;
					TempTree = FifthTree;
				}
				
				//if the temp object is actually a rover
				if (TempTree != 7) {
					//if that rover is already selected then deselect it
					if (TempRover == SelectedRover && SelectedRover != null) {
						print ("Deselecting " + SelectedRover.name);
						SelectedRover.renderer.material = RoverAlive;
						SelectedRover = null;
						SelectedGround = null;
						SelectedAlive = false;
						SelectedTree = 7;
						TempRover = null;
						TempGround = null;
						TempAlive = false;
						TempTree = 7;
					}
					//if it isn't selected then select it iff it's alive
					else if (TempAlive == true) {
						if (SelectedRover != null)
							SelectedRover.renderer.material = RoverAlive;
						SelectedRover = TempRover;
						SelectedGround = TempGround;
						SelectedAlive = TempAlive;
						SelectedTree = TempTree;
						SelectedRover.renderer.material = RoverSelected;
						TempRover = null;
						TempGround = null;
						TempAlive = false;
						TempTree = 7;
						print ("Selecting " + SelectedRover.name);
					}
					//if it's a rover but not alive play negative sound
					else {
						print ("This rover is not alive.");
						audio.PlayOneShot (incorrectSound);
					}
				}
				//this means that the object wasn't a rover so check if it's valid ground
				else if (TempRover == CuriosityGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == CuriosityRover) {
							print ("Curiosity was moved.");
							CuriosityRover.transform.position = hit.point;
							//Save position
							PlayerPrefsX.SetVector3("CuriosityRover", CuriosityRover.transform.position);
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == SpiritGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == SpiritRover) {
							if (OpportunityAlive == true && OpportunityTree <= SpiritTree) {
								float distance = Vector3.Distance (hit.point, OpportunityRover.transform.position);
								if (distance <0.45) {
									print ("Distance bt Spirit and Op is: " + distance);
									if (SpiritTree > (OpportunityTree + 1))
										SpiritTree = (OpportunityTree + 1);
									SpiritRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SpiritRover", SpiritRover.transform.position);
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							if (SojournerAlive == true && SojournerTree <= SpiritTree) {
								float distance = Vector3.Distance (hit.point, SojournerRover.transform.position);
								if (distance <0.45) {
									print ("Distance bt Spirit and Sj is: " + distance);
									if (SpiritTree > (SojournerTree + 1))
										SpiritTree = (SojournerTree + 1);
									SpiritRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SpiritRover", SpiritRover.transform.position);
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}		
							if (FifthAlive == true && FifthTree <= SpiritTree) {
								float distance = Vector3.Distance (hit.point, FifthRover.transform.position);
								if (distance <0.45) {
									print ("Distance bt Spirit and Ft is: " + distance);
									if (SpiritTree > (FifthTree + 1))
										SpiritTree = (FifthTree + 1);
									SpiritRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SpiritRover", SpiritRover.transform.position);
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (hit.point, CuriosityRover.transform.position);
								if (distance <0.45) {
									print ("Distance bt Spirit and Cr is: " + distance);
									if (SpiritTree > (CuriosityTree + 1))
										SpiritTree = (CuriosityTree + 1);
									SpiritRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SpiritRover", SpiritRover.transform.position);
									print ("Spirit was moved and it's tree is " + SpiritTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == OpportunityGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
						if (SelectedTree != 7) {
							if (SelectedRover == OpportunityRover) {
							if (SpiritAlive == true && SpiritTree <= OpportunityTree) {
								float distance = Vector3.Distance (hit.point, SpiritRover.transform.position);
									if (distance <0.45) {
										print ("Distance bt Op and Sp is: " + distance);
										if (OpportunityTree > (SpiritTree + 1))
											OpportunityTree = (SpiritTree + 1);
										OpportunityRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("OpportunityRover", OpportunityRover.transform.position);
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}
							if (SojournerAlive == true && SojournerTree <= OpportunityTree) {
								float distance = Vector3.Distance (hit.point, SojournerRover.transform.position);
									if (distance <0.45) {
										print ("Distance bt Op and Sj is: " + distance);
										if (OpportunityTree > (SojournerTree + 1))
											OpportunityTree = (SojournerTree + 1);
										OpportunityRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("OpportunityRover", OpportunityRover.transform.position);
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}		
							if (FifthAlive == true && FifthTree <= OpportunityTree) {
									float distance = Vector3.Distance (hit.point, FifthRover.transform.position);
									if (distance <0.45) {
										print ("Distance bt Op and Ft is: " + distance);
										if (OpportunityTree > (FifthTree + 1))
											OpportunityTree = (FifthTree + 1);
										OpportunityRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("OpportunityRover", OpportunityRover.transform.position);
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}	
								if (CuriosityAlive == true) {
								float distance = Vector3.Distance (hit.point, CuriosityRover.transform.position);
									if (distance <0.45) {
										print ("Distance bt Op and Cr is: " + distance);
										if (OpportunityTree > (CuriosityTree + 1))
											OpportunityTree = (CuriosityTree + 1);
										OpportunityRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("OpportunityRover", OpportunityRover.transform.position);
										print ("Opportunity was moved and it's tree is " + OpportunityTree);
									}
								}
								else {
									print ("Could not be moved.");
								}
							} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				} 
				else if (TempRover == SojournerGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == SojournerRover) {
							if (SpiritAlive == true && SpiritTree <= SojournerTree) {
								float distance = Vector3.Distance (hit.point, SpiritRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (SojournerTree > (SpiritTree + 1))
										SojournerTree = (SpiritTree + 1);
									SojournerRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SojournerRover", SojournerRover.transform.position);
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							if (OpportunityAlive == true && OpportunityTree <= SojournerTree) {
								float distance = Vector3.Distance (hit.point, OpportunityRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (SojournerTree > (OpportunityTree + 1))
										SojournerTree = (OpportunityTree + 1);
									SojournerRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SojournerRover", SojournerRover.transform.position);
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}		
							if (FifthAlive == true && FifthTree <= SojournerTree) {
								float distance = Vector3.Distance (hit.point, FifthRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (SojournerTree > (FifthTree + 1))
										SojournerTree = (FifthTree + 1);
									SojournerRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SojournerRover", SojournerRover.transform.position);
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (hit.point, CuriosityRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (SojournerTree > (CuriosityTree + 1))
										SojournerTree = (CuriosityTree + 1);
									SojournerRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("SojournerRover", SojournerRover.transform.position);
									print ("Sojourner was moved and it's tree is " + SojournerTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				}
				else if (TempRover == FifthGround) {
					TempRover = null;
					TempGround = null;
					TempAlive = false;
					TempTree = 7;
					if (SelectedTree != 7) {
						if (SelectedRover == FifthRover) {
							if (SpiritAlive == true && SpiritTree <= FifthTree) {
								float distance = Vector3.Distance (hit.point, SpiritRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (FifthTree > (SpiritTree + 1))
										FifthTree = (SpiritTree + 1);
									FifthRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("FifthRover", FifthRover.transform.position);
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}
							if (SojournerAlive == true && SojournerTree <= FifthTree) {
								float distance = Vector3.Distance (hit.point, SojournerRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (FifthTree > (SojournerTree + 1))
										FifthTree = (SojournerTree + 1);
									FifthRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("FifthRover", FifthRover.transform.position);
									print ("Fifth was moved and it's tree is " + OpportunityTree);
								}
							}		
							if (OpportunityAlive == true && OpportunityTree <= FifthTree) {
								float distance = Vector3.Distance (hit.point, OpportunityRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (FifthTree > (OpportunityTree + 1))
										FifthTree = (OpportunityTree + 1);
									FifthRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("FifthRover", FifthRover.transform.position);
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}	
							if (CuriosityAlive == true) {
								float distance = Vector3.Distance (hit.point, CuriosityRover.transform.position);
								if (distance <0.45) {
				print ("Distance is: " + distance);
									if (FifthTree > (CuriosityTree + 1))
										FifthTree = (CuriosityTree + 1);
									FifthRover.transform.position = hit.point;
									//Save position
									PlayerPrefsX.SetVector3("FifthRover", FifthRover.transform.position);
									print ("Fifth was moved and it's tree is " + FifthTree);
								}
							}
							else {
								print ("Could not be moved.");
							}
						} 
						else {
							audio.PlayOneShot (incorrectSound);
							print ("Invalid position.");
						}
					}
				}
				else if (TempRover == InvalidGround) {
					if (SelectedTree != 7) {
						audio.PlayOneShot (incorrectSound);
						print ("Invalid ground.");
					}
				}
				//if it isn't a rover and isn't valid ground and it isn't invalid ground then it's offscreen
				else {
					//if (SelectedRover != null) {
//						print ("Do the thing.");
//
//						PersistantGlobalScript.interactionEnabled = true;
//						PersistantGlobalScript.mouseLookEnabled = true;
//						PersistantGlobalScript.movementEnabled = true;
//						player.gameObject.SetActive(true);
//						instructions.SetActive(false);
//
//						
//						reticle.SetActive(true);
//						FPCscript.lockCursor = true;
//						PersistantGlobalScript.minigameActive = false;
//						RoverPuzzleObject.collider.enabled=true;
//						mainCamera.SetActive(true);
//						miniCam.SetActive(false);
//						Screen.lockCursor = true;
//						RoverPuzzleScript.enabled=false;
					//}
				}
			}
		}
	}
																													
	void Alive() {
		//check alive and "tree" position for spirit
		SpiritTree = 7;
		if (OpportunityAlive == true && OpportunityTree <= SpiritTree) {
			float distance = Vector3.Distance (SpiritRover.transform.position, OpportunityRover.transform.position);
			if (distance < 0.45) {
				SpiritAlive = true;
				SpiritPing.SetActive(true);
				if (SelectedRover != SpiritRover)
					SpiritRover.renderer.material = RoverAlive;
				if(SpiritTree > (OpportunityTree+1))
					SpiritTree = (OpportunityTree+1);
			}
		}
		if (SojournerAlive == true && SojournerTree <= SpiritTree) {
 			float distance = Vector3.Distance (SpiritRover.transform.position, SojournerRover.transform.position);
			if (distance <0.45) {
				SpiritAlive = true;
				SpiritPing.SetActive(true);
				if (SelectedRover != SpiritRover)
					SpiritRover.renderer.material = RoverAlive;
				if(SpiritTree > (SojournerTree+1))
					SpiritTree = (SojournerTree+1);
			}
		}
		if (CuriosityAlive == true && CuriosityTree <= SpiritTree) {
			float distance = Vector3.Distance (SpiritRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.45) {;
				SpiritAlive = true;
				SpiritPing.SetActive(true);
				if (SelectedRover != SpiritRover)
					SpiritRover.renderer.material = RoverAlive;
				if(SpiritTree > (CuriosityTree+1))
					SpiritTree = (CuriosityTree+1);
			}
		}
		if (FifthAlive == true && FifthTree <= SpiritTree) {
			float distance = Vector3.Distance (SpiritRover.transform.position, FifthRover.transform.position);
			if (distance <0.45) {
				SpiritAlive = true;
				SpiritPing.SetActive(true);
				if (SelectedRover != SpiritRover)
					SpiritRover.renderer.material = RoverAlive;
				if(SpiritTree > (FifthTree+1))
					SpiritTree = (FifthTree+1);
			}
		}
		if (SpiritTree == 7) {
			SpiritAlive = false;
			SpiritPing.SetActive(false);
			SpiritRover.renderer.material = RoverColor;
		}
		//check alive and "tree" position for opportunity 
		OpportunityTree = 7;
		if (SpiritAlive == true && SpiritTree <= OpportunityTree) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, SpiritRover.transform.position);
			if (distance <0.45) {
				OpportunityAlive = true;
				OpportunityPing.SetActive(true);
				if (SelectedRover != OpportunityRover)
					OpportunityRover.renderer.material = RoverAlive;
				if(OpportunityTree > (SpiritTree+1))
					OpportunityTree = (SpiritTree+1);
			}
		}
		if (SojournerAlive == true && SojournerTree <= OpportunityTree) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, SojournerRover.transform.position);
			if (distance <0.45) {
				OpportunityAlive = true;
				OpportunityPing.SetActive(true);
				if (SelectedRover != OpportunityRover)
					OpportunityRover.renderer.material = RoverAlive;
				if(OpportunityTree > (SojournerTree+1))
					OpportunityTree = (SojournerTree+1);
			}
		}
		if (CuriosityAlive == true && CuriosityTree <= OpportunityTree) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.45) {
				OpportunityAlive = true;
				OpportunityPing.SetActive(true);
				if (SelectedRover != OpportunityRover)
					OpportunityRover.renderer.material = RoverAlive;
				if(OpportunityTree > (CuriosityTree+1))
					OpportunityTree = (CuriosityTree+1);
			}
		}
		if (FifthAlive == true && FifthTree <= OpportunityTree) {
			float distance = Vector3.Distance (OpportunityRover.transform.position, FifthRover.transform.position);
			if (distance <0.45) {
				OpportunityAlive = true;
				OpportunityPing.SetActive(true);
				if (SelectedRover != OpportunityRover)
					OpportunityRover.renderer.material = RoverAlive;
				if(OpportunityTree > (FifthTree+1))
					OpportunityTree = (FifthTree+1);
			}
		}
		if (OpportunityTree == 7) {
			OpportunityAlive = false;
			OpportunityPing.SetActive(false);
			OpportunityRover.renderer.material = RoverColor;
		}
		SojournerTree = 7;
		//check alive and "tree" position and win condition for sojourner
		if (SpiritAlive == true && SpiritTree <= SojournerTree) {
			float distance = Vector3.Distance (SojournerRover.transform.position, SpiritRover.transform.position);
			if (distance <0.45) {
				SojournerAlive = true;
				SojournerPing.SetActive(true);
				if (SelectedRover != SojournerRover)
					SojournerRover.renderer.material = RoverAlive;
				if(SojournerTree > (SpiritTree + 1))
					SojournerTree = (SpiritTree + 1);
			}
		}
		if (OpportunityAlive == true && OpportunityTree <= SojournerTree) {
			float distance = Vector3.Distance (SojournerRover.transform.position, OpportunityRover.transform.position);
			if (distance <0.45) {
				SojournerAlive = true;
				SojournerPing.SetActive(true);
				if (SelectedRover != SojournerRover)
					SojournerRover.renderer.material = RoverAlive;
				if(SojournerTree > (OpportunityTree + 1))
					SojournerTree = (OpportunityTree + 1);
			}
		}
		if (CuriosityAlive == true && CuriosityTree <= SojournerTree) {
			float distance = Vector3.Distance (SojournerRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.45) {
				SojournerAlive = true;
				SojournerPing.SetActive(true);
				if (SelectedRover != SojournerRover)
					SojournerRover.renderer.material = RoverAlive;
				if(SojournerTree > (CuriosityTree + 1))
					SojournerTree = (CuriosityTree + 1);
			}
		}
		if (FifthAlive == true && FifthTree <= SojournerTree) {
			float distance = Vector3.Distance (SojournerRover.transform.position, FifthRover.transform.position);
			if (distance <0.45) {
				SojournerAlive = true;
				SojournerPing.SetActive(true);
				if (SelectedRover != SojournerRover)
					SojournerRover.renderer.material = RoverAlive;
				if(SojournerTree > (FifthTree+1))
					SojournerTree = (FifthTree+1);
			}
		}
		if (YouWon == false ) {
			float distance = Vector3.Distance (SojournerRover.transform.position, FinishObj.transform.position);
			bool dist = (distance < 0.45);
			print ("Checking false, distance is: " + distance + ", " + dist);
			if (distance < 0.58) {
				audio.PlayOneShot (winSound);
				FinObj.renderer.material = FinWin;
				print ("You win!");
				YouWon = true;
				//Save win state
				PlayerPrefsX.SetBool("RoverWin", true);

//				PersistantGlobalScript.interactionEnabled = true;
//				PersistantGlobalScript.mouseLookEnabled = true;
//				PersistantGlobalScript.movementEnabled = true;
//
//				PersistantGlobalScript.minigameActive = false;
//				RoverPuzzleObject.collider.enabled=true;
//				mainCamera.SetActive(true);
//				miniCam.SetActive(false);
//				player.gameObject.SetActive(true);
//				FPCscript.lockCursor = true;
//
//				Screen.lockCursor = true;
//				instructions.SetActive(false);
//				RoverPuzzleScript.enabled=false;
//
//				reticle.SetActive(true);
				//Exit ();
				JournalScript.addItem(journalEntry);
				finalDoor.isAirLocked = false;
				//probably trigger some dialogue thing
				dialogue.SetActive(true);
				nextLog.SetActive(true);
			}
		}
		if (SojournerTree == 7) {
			SojournerAlive = false;
			SojournerPing.SetActive(false);
			SojournerRover.renderer.material = RoverColor;
		}
		//check alive and "tree" position for fifth
		FifthTree = 7;
		if (SpiritAlive == true && SpiritTree <= FifthTree) {
			float distance = Vector3.Distance (FifthRover.transform.position, SpiritRover.transform.position);
			if (distance <0.45) {
				FifthAlive = true;
				FifthPing.SetActive(true);
				if (SelectedRover != FifthRover)
					FifthRover.renderer.material = RoverAlive;
				if(FifthTree > (SpiritTree + 1))
					FifthTree = (SpiritTree + 1);
			}
		}
		if (OpportunityAlive == true && OpportunityTree <= FifthTree) {
			float distance = Vector3.Distance (FifthRover.transform.position, OpportunityRover.transform.position);
			if (distance <0.45) {
				FifthAlive = true;
				FifthPing.SetActive(true);
				if (SelectedRover != FifthRover)
					FifthRover.renderer.material = RoverAlive;
				if(FifthTree > (OpportunityTree + 1))
					FifthTree = (OpportunityTree + 1);
			}
		}
		if (CuriosityAlive == true && CuriosityTree <= FifthTree) {
			float distance = Vector3.Distance (FifthRover.transform.position, CuriosityRover.transform.position);
			if (distance <0.45) {
				FifthAlive = true;
				FifthPing.SetActive(true);
				if (SelectedRover != FifthRover)
					FifthRover.renderer.material = RoverAlive;
				if(FifthTree > (CuriosityTree + 1))
					FifthTree = (CuriosityTree + 1);
			}
		}
		if (SojournerAlive == true && SojournerTree <= FifthTree) {
			float distance = Vector3.Distance (FifthRover.transform.position, SojournerRover.transform.position);
			if (distance <0.45) {
				FifthAlive = true;
				FifthPing.SetActive(true);
				if (SelectedRover != FifthRover)
					FifthRover.renderer.material = RoverAlive;
				if(FifthTree > (SojournerTree+1))
					FifthTree = (SojournerTree+1);
			}
		}
		if (FifthTree == 7) {
			FifthAlive = false;
			FifthPing.SetActive(false);
			FifthRover.renderer.material = RoverColor;
		}
		//no need to check for curiosity as it is alwasy root
	}
}