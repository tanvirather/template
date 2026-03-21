DO $$
BEGIN
	IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_product_numeric_type_updated_by_id_identity_users') THEN
  	ALTER TABLE product.numeric_type ADD CONSTRAINT fk_product_numeric_type_updated_by_id_identity_users FOREIGN KEY (updated_by_id) REFERENCES identity.users (id);
  END IF;
END $$;
