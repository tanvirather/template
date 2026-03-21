CREATE TABLE IF NOT EXISTS product.numeric_type ();
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS id uuid NOT NULL;
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS updated_by_id uuid NOT NULL;
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS updated timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS smallint_type smallint;
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS integer_type integer;
ALTER TABLE product.numeric_type ADD COLUMN IF NOT EXISTS bigint_type bigint;
