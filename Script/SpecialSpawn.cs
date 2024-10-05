using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialSpawn : MonoBehaviour
{
    public ParticleSystem particleSystem2;
    public GameObject GameObject;
    public TextMeshProUGUI mes;
    public AudioClip appearSe;
    private bool isMoving = false;
    private bool isParth = false;
    private float movePos = 0;
    private int cnt = 1;
    private Vector3 randomSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeParth(60.0f));
        StartCoroutine(WaitBeforeMove(65.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isParth)
        {
            // �p�[�e�B�N���������_���Ȉʒu�ɐ���
            randomSpawnPos = new Vector3(Random.Range(-43, -32), 0, Random.Range(-42, 66));
            ParticleSystem particles = Instantiate(particleSystem2, randomSpawnPos, Quaternion.identity);
            particles.Play();

            // �p�[�e�B�N�����Đ�����10�b��ɏ���
            StartCoroutine(DestroyParticleAfterDelay(particles, 10.0f));

            // �p�[�e�B�N���̌�ɃA�C�e���𐶐����邽�߂̃R���[�`�����J�n
            StartCoroutine(SpawnItemAfterParticles(particles.main.duration));

            isParth = false; // ���̃t���O�����Z�b�g
        }

        if (isMoving)
        {
            if (movePos >= -1300)
            {
                movePos -= 1.5f;
                mes.rectTransform.position = new Vector3(movePos, 50, 0);
            }
        }
    }

    IEnumerator WaitBeforeMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }

    IEnumerator WaitBeforeParth(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isParth = true;
    }

    IEnumerator DestroyParticleAfterDelay(ParticleSystem particles, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particles.gameObject);
    }

    IEnumerator SpawnItemAfterParticles(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);

        if (cnt == 1)
        {
            Instantiate(GameObject, randomSpawnPos, Quaternion.identity);
            Debug.Log("Spawn");
            AudioSource.PlayClipAtPoint(appearSe, new Vector3(0, 0, 0));

            movePos = Screen.width;
            cnt--;
        }
    }
}
