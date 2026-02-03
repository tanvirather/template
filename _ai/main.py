from transformers import AutoTokenizer, AutoModelForCausalLM
from pathlib import Path

MODEL_NAME = "deepseek-ai/deepseek-coder-1.3b-base"
CACHE_DIR = Path("./tmp")

# load model and tokenizer
model = AutoModelForCausalLM.from_pretrained(MODEL_NAME, cache_dir=CACHE_DIR, trust_remote_code=True)
tokenizer = AutoTokenizer.from_pretrained(MODEL_NAME, cache_dir=CACHE_DIR, trust_remote_code=True)

prompt = "Write a hello world program in c#"
inputs = tokenizer(prompt, return_tensors="pt").to(model.device)
outputs = model.generate(**inputs, max_length=128)
print(tokenizer.decode(outputs[0], skip_special_tokens=True))

