# Run Python

```sh
sudo apt install --yes python3 python3-pip python3-venv

rm -rf .venv
python3 -m venv .venv
source .venv/bin/activate
deactivate

pip install torch transformers accelerate huggingface_hub
```
